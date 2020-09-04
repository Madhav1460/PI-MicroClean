using Microclean.CommandQueryLayer.Commands;
using Microclean.CommandQueryLayer.Models;
using Microclean.CommandQueryLayer.Queries;
using Microclean.CommandQueryLayer.QueryModels;
using Microclean.DataAccessLayer.Core;
using Microclean.DataModel;
using Microclean.DataModel.Models;
using Microclean.RepositoryLayer.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Utilities.Logger;
using Utilities.Results;

namespace Microclean.RepositoryLayer.Repositories
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly IUnitOfWork<MicrocleanDbContext> _unit;

        public QuestionRepository(IUnitOfWork<MicrocleanDbContext> unit)
        {
            _unit = unit;
        }
        public async Task<ApiResultCode> CreateQuestionAsync(CreateQuestionCommand request)
        {
            string strWithouthtm = Regex.Replace(request.Name, "<.*?>", string.Empty);
            bool isQuestionExist = _unit.GetRepository<Tblquestion>().Exists(t => t.CategoryId == request.CategoryId && t.Name == strWithouthtm).Result.UserObject;
            if (isQuestionExist)
            {
                return new ApiResultCode(ApiResultType.Error, messageText: "Question already exists for the selected test.!!");
            }
            Tblquestion question = null;
            Tblquestionoptions tblquestionoptions = null;
            if (request.Id.HasValue && request.Id > 0)
            {
                question = new Tblquestion();
                question.Id = request.Id.Value;
                question.Name = request.Name;
                question.CategoryId = request.CategoryId;
                question.QuestionDuration = request.DurationId;
                question.LastUpdatedBy = request.CurrentUserId;
                question.LastUpdateDate = DateTime.Now;
                _unit.Context.Tblquestion.Attach(question);
                _unit.Context.Entry(question).Property(t => t.Name).IsModified = true;
                _unit.Context.Entry(question).Property(t => t.CategoryId).IsModified = true;
                _unit.Context.Entry(question).Property(t => t.QuestionDuration).IsModified = true;
                _unit.Context.Entry(question).Property(t => t.LastUpdateDate).IsModified = true;
                _unit.Context.Entry(question).Property(t => t.LastUpdatedBy).IsModified = true;
                foreach (var item in request.Options)
                {
                    tblquestionoptions = new Tblquestionoptions();
                    tblquestionoptions.Id = item.Id;
                    tblquestionoptions.Name = item.Name;
                    tblquestionoptions.IsMatched = (byte?)(item.IsMatched == true ? 1 : 0);
                    tblquestionoptions.LastUpdateDate = DateTime.Now;
                    tblquestionoptions.LastUpdatedBy = request.CurrentUserId;
                    _unit.Context.Tblquestionoptions.Attach(tblquestionoptions);
                    _unit.Context.Entry(tblquestionoptions).Property(t => t.Name).IsModified = true;
                    _unit.Context.Entry(tblquestionoptions).Property(t => t.IsMatched).IsModified = true;
                    _unit.Context.Entry(tblquestionoptions).Property(t => t.LastUpdateDate).IsModified = true;
                    _unit.Context.Entry(tblquestionoptions).Property(t => t.LastUpdatedBy).IsModified = true;
                }

            }
            else
            {
                question = new Tblquestion();
                question.Name = request.Name;
                question.CategoryId = request.CategoryId;
                question.QuestionDuration = request.DurationId;
                question.InsertDate = DateTime.Now;
                question.NoOfOptions = request.NoOfOption;
                question.InsertedBy = request.CurrentUserId;
                question.LastUpdatedBy = request.CurrentUserId;
                question.LastUpdateDate = DateTime.Now;
                question.Status = 1;
                foreach (var item in request.Options)
                {
                    question.Tblquestionoptions.Add(new Tblquestionoptions
                    {
                        Name = item.Name,
                        IsMatched = (byte?)(item.IsMatched == true ? 1 : 0),
                        Status = 1,
                        InsertDate = DateTime.Now,
                        InsertedBy = request.CurrentUserId,
                        LastUpdatedBy = request.CurrentUserId,
                        LastUpdateDate = DateTime.Now
                    });
                }
                _ = _unit.GetRepository<Tblquestion>().Add(question);
            }

            var result = await _unit.SaveChangesAsync();
            if (result.ResultType == ApiResultType.Success)
            {
                return new ApiResultCode(ApiResultType.Success, messageText: "Added");
            }
            return new ApiResultCode(ApiResultType.Error, messageText: "Added");
        }

        public async Task<ApiResultCode> DeleteQuestionAsync(DeleteQuestionCommand datamodel)
        {
            int NooofQuestion = 0;
            var tblquestion = _unit.Context.Tblquestion.Where(p => p.Id == datamodel.Id).FirstOrDefault();

            var count = await _unit.Context.Tblquestion.Where(t => t.CategoryId == tblquestion.CategoryId && t.Status == 1).CountAsync();
            if (_unit.Context.Tbltest.Where(t => t.CategoryId == tblquestion.CategoryId).Any())
                NooofQuestion = _unit.Context.Tbltest.Where(t => t.CategoryId == tblquestion.CategoryId).Max(t => t.NoOfQuestion).Value;

            if (NooofQuestion >= count)
                return new ApiResultCode(ApiResultType.Error, messageText: "Requested qestion can not delete!");

            var query = _unit.Context.Tblquestionoptions.Where(q => q.QuestionId == datamodel.Id).ToList();
            tblquestion.Status = 0;

            tblquestion.LastUpdatedBy = datamodel.CurrentUserId;
            tblquestion.LastUpdateDate = DateTime.Now;

            _unit.Context.Tblquestion.Attach(tblquestion);
            _unit.Context.Entry(tblquestion).Property(t => t.Status).IsModified = true;
            _unit.Context.Entry(tblquestion).Property(t => t.LastUpdatedBy).IsModified = true;
            _unit.Context.Entry(tblquestion).Property(t => t.LastUpdateDate).IsModified = true;

            foreach (var item in query)
            {
                item.Status = 0;
                item.LastUpdateDate = DateTime.Now;
                item.LastUpdatedBy = datamodel.CurrentUserId;
                _ = _unit.GetRepository<Tblquestionoptions>().Update(item);
            }
            var result = await _unit.SaveChangesAsync();
            if (result.ResultType == ApiResultType.Success)
            {
                return new ApiResultCode(ApiResultType.Success, messageText: "Question Deleted!");
            }
            return new ApiResultCode(ApiResultType.Error, messageText: "Can not Delete!");
        }

        public async Task<ApiResult<IList<QuestionWithListQuery>>> GetQuestionDetailAsync(GetQuestionFilterQuery filter)
        {
            try
            {
                var query = _unit.Context.Tblquestion.Include(t => t.Category).AsQueryable();
                if (filter.Filter.CategoryId > 0)
                {
                    var result = await query.Select(t => new QuestionWithListQuery
                    {
                        Id = t.Id,
                        CategoryId = t.CategoryId,
                        Category = t.Category.Name,
                        Question = t.Name,
                        Status = t.Status
                    }).Where(t => t.Status == 1 && t.CategoryId == filter.Filter.CategoryId).ToListAsync();
                    if (result != null && result.Count > 0)
                    {
                        return new ApiResult<IList<QuestionWithListQuery>>(new ApiResultCode(ApiResultType.Success), result);
                    }
                }
                else
                {
                    var result = await query.Select(t => new QuestionWithListQuery
                    {
                        Id = t.Id,
                        CategoryId = t.CategoryId,
                        Category = t.Category.Name,
                        Question = t.Name,
                        Status = t.Status
                    }).Where(t => t.Status == 1).ToListAsync();
                    if (result != null && result.Count > 0)
                    {
                        return new ApiResult<IList<QuestionWithListQuery>>(new ApiResultCode(ApiResultType.Success), result);
                    }
                }

            }
            catch (Exception ex)
            {
                ErrorTrace.Logger(LogArea.RepositoryLayer, ex);
                return new ApiResult<IList<QuestionWithListQuery>>(new ApiResultCode(ApiResultType.Error, messageText: "Error while geting data"));
            }
            return new ApiResult<IList<QuestionWithListQuery>>(new ApiResultCode(ApiResultType.Error, messageText: "no data found"));
        }

        public async Task<ApiResult<CreateQuestionCommand>> GetQuestionOptionByIdAsync(GetQuestionOptionByIdQuery filter)
        {
            try
            {
                var questionqultion = await _unit.Context.Tblquestion.Include(t => t.Category).Include(t => t.Tblquestionoptions).Where(p => p.Id == filter.Filter.QuestionId && p.Status == 1).FirstOrDefaultAsync();
                var result = new CreateQuestionCommand
                {
                    Id = questionqultion.Id,
                    Name = questionqultion.Name,
                    NoOfOption = questionqultion.NoOfOptions.HasValue ? questionqultion.NoOfOptions.Value : Convert.ToInt16(0),
                    CategoryId = questionqultion.CategoryId.Value,
                    DurationId = questionqultion.QuestionDuration.Value,
                    Options = questionqultion.Tblquestionoptions.Select(x => new CreateOptionCommand
                    {
                        Id = x.Id,
                        Name = x.Name,
                        IsMatched = x.IsMatched == 1 ? true : false
                    }).ToList()
                };
                return new ApiResult<CreateQuestionCommand>(new ApiResultCode(ApiResultType.Success), result);
            }
            catch (Exception ex)
            {
                ErrorTrace.Logger(LogArea.RepositoryLayer, ex);
                return new ApiResult<CreateQuestionCommand>(new ApiResultCode(ApiResultType.Error, messageText: "Error while geting data"));
            }
        }
    }
}
