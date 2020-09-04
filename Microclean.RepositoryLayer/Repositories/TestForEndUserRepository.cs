using Microclean.CommandQueryLayer.Commands;
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
using System.Threading.Tasks;
using Utilities.Logger;
using Utilities.Results;
using Microclean.RepositoryLayer.Extensions;

namespace Microclean.RepositoryLayer.Repositories
{
    public class TestForEndUserRepository : ITestForEndUserRepository
    {
        private readonly IUnitOfWork<MicrocleanDbContext> _unit;

        public TestForEndUserRepository(IUnitOfWork<MicrocleanDbContext> unit)
        {
            _unit = unit;
        }

        public async Task<ApiResult<IEnumerable<TestListQueryModel>>> GetTestListync(GetTestsForEndUser filter)
        {
            var result = await _unit.Context.Tbltestuserright.Include(t => t.Test).ThenInclude(t => t.Tblusersubmitedanswer)
                .Where(p => p.UserId == filter.CurrentUserId && p.Status == 1 && p.Test.Status == 1)
                .Select(t => new TestListQueryModel
                {
                    Test = t.Test.Name,
                    TestId = t.TestId.Value,
                    IsTestDone = t.Test.Tblusersubmitedanswer.Where(p => p.UserId == filter.CurrentUserId.Value && p.TestId == t.TestId.Value && p.IsMatched.HasValue).Select(t => t.IsMatched).FirstOrDefault()
                }).ToListAsync();

            if (result.Count > 0)
                return new ApiResult<IEnumerable<TestListQueryModel>>(new ApiResultCode(ApiResultType.Success), result);

            return new ApiResult<IEnumerable<TestListQueryModel>>(new ApiResultCode(ApiResultType.Error, messageText: "No data found"));
        }

        public async Task<ApiResult<TestQueryModel>> GetTestQuestionOptionAsync(GetTestQuestionOptionForEndUserByTestId filter)
        {
            TestQueryModel modeltest = null;
            try
            {
                if (_unit.Context.Tblusersubmitedanswer.Any(t => t.UserId == filter.CurrentUserId && t.TestId == filter.Filter.TestId.Value && t.IsMatched.HasValue))
                    return new ApiResult<TestQueryModel>(new ApiResultCode(ApiResultType.Error, messageText: "You have already done this test."));

                if (!_unit.Context.Tblusersubmitedanswer.Any(t => t.UserId == filter.CurrentUserId && t.TestId == filter.Filter.TestId.Value))
                {
                    var noofquestionAndCategoryId = await _unit.Context.Tbltest.Where(p => p.Id == filter.Filter.TestId.Value).
                        Select(t => new { t.NoOfQuestion.Value, t.CategoryId }).FirstOrDefaultAsync();
                    var query = await (from t in _unit.Context.Tbltest
                                       join ttr in _unit.Context.Tbltestuserright on t.Id equals ttr.TestId
                                       join tcat in _unit.Context.Tblcategory on t.CategoryId equals tcat.Id
                                       join tq in _unit.Context.Tblquestion on tcat.Id equals tq.CategoryId
                                       join to in _unit.Context.Tblquestionoptions on tq.Id equals to.QuestionId
                                       where ttr.TestId == filter.Filter.TestId && ttr.UserId == filter.CurrentUserId.Value && ttr.Status == 1 && tq.Status == 1
                                       && tq.CategoryId == noofquestionAndCategoryId.CategoryId.Value && t.CategoryId == noofquestionAndCategoryId.CategoryId.Value
                                       select new Tblusersubmitedanswer
                                       {
                                           TestId = ttr.TestId,
                                           UserId = ttr.UserId,
                                           QuestionId = tq.Id,
                                           OptionId = to.Id,
                                           InsertDate = DateTime.Now,
                                           LastUpdateDate = DateTime.Now
                                       }).ToListAsync();
                    var allAvailbleQuestionIds = query.Select(t => t.QuestionId.Value).Distinct();
                    int questioncout = allAvailbleQuestionIds.Count();
                    List<long> ids = new List<long>();
                    int number;
                    if (noofquestionAndCategoryId.Value == questioncout)
                    {
                        ids.AddRange(allAvailbleQuestionIds);
                    }
                    else
                    {
                        for (int i = 0; i < noofquestionAndCategoryId.Value; i++)
                        {
                            do
                            {
                                number = new Random().Next(1,Convert.ToInt32(allAvailbleQuestionIds.Max()));
                            } while (!allAvailbleQuestionIds.Contains(number) || ids.Contains(number)); //prevent duplicate number ids.Contains(number)
                            ids.Add(number);
                        }
                    }
                    var questionsquery = query.Where(t => ids.Contains(t.QuestionId.Value)).ToList();
                    questionsquery.ProduceShuffle(new Random());
                    _unit.Context.Tblusersubmitedanswer.AddRange(questionsquery);
                    var saveresult = await _unit.SaveChangesAsync();
                }
                var testquery = await _unit.Context.Tbltest.Where(p => p.Id == filter.Filter.TestId).Include(t => t.Tblusersubmitedanswer).ThenInclude(t => t.Question).
                        ThenInclude(t => t.Tblquestionoptions).Include(t => t.Tbltestuserright).ThenInclude(t => t.User).FirstOrDefaultAsync();

                if (testquery != null)
                {

                    modeltest = new TestQueryModel();
                    modeltest.TestName = testquery.Name;
                    modeltest.UserId = filter.CurrentUserId.Value;
                    modeltest.UserName = testquery.Tblusersubmitedanswer.Where(t => t.UserId == filter.CurrentUserId).Select(t => (t.User.FirstName + " " + t.User.MiddleName + " " + t.User.LastName)).FirstOrDefault();
                    modeltest.TestId = testquery.Id;
                    modeltest.Questions = testquery.Tblusersubmitedanswer.Where(t => t.UserId == filter.CurrentUserId).GroupBy(t => t.QuestionId).Select(q => new TestQuestionQueryModel
                    {
                        Question = q.FirstOrDefault().Question.Name,
                        QuestionId = q.FirstOrDefault().Question.Id,
                        TakeActionDuration = q.FirstOrDefault().Question.QuestionDuration.Value,
                        Options = q.FirstOrDefault().Question.Tblusersubmitedanswer.Where(t => t.UserId == filter.CurrentUserId && t.QuestionId == q.FirstOrDefault().Question.Id).
                        Select(t => new TestQuestionOptionsQueryModel
                        {
                            SubmmitedAnswerId = t.Id,
                            OptionId = t.OptionId.Value,
                            Option = t.Option.Name,
                        }).ToList()
                    }).Distinct().ToList();
                    return new ApiResult<TestQueryModel>(new ApiResultCode(ApiResultType.Success), modeltest);
                }
                return new ApiResult<TestQueryModel>(new ApiResultCode(ApiResultType.Error, messageText: "no data found"));
            }
            catch (Exception ex)
            {
                ErrorTrace.Logger(LogArea.RepositoryLayer, ex);
                return new ApiResult<TestQueryModel>(new ApiResultCode(ApiResultType.Error, messageText: "Error while geting data"));
            }
        }

        public async Task<ApiResultCode> SubmitTestAsync(SubmitTestCommand command)
        {
            if (_unit.Context.Tblusersubmitedanswer.Any(t => t.UserId == command.UserId && t.TestId == command.TestId && t.IsMatched.HasValue))
                return new ApiResultCode(ApiResultType.Error, messageText: "You have already done this test.");

            List<Tblusersubmitedanswer> tblusersubmitedanswers = new List<Tblusersubmitedanswer>();
            Tblusersubmitedanswer answer = null;
            var count = command.QuestionsCommand.Where(t => t.OptionsCommand.Count > 0).FirstOrDefault();
            if (count == null)
                return new ApiResultCode(ApiResultType.Error, messageText: "You have not attempted any questions. Please try again.");

            foreach (var item in command.QuestionsCommand)
            {
                var options = item.OptionsCommand.Where(t => t.IsMatched == true).ToList();
                foreach (var option in options)
                {
                    answer = new Tblusersubmitedanswer();
                    answer.Id = option.SubmitedAnswerId;
                    answer.IsMatched = 1;
                    answer.LastUpdateDate = DateTime.Now;
                    answer.Status = 1;
                    _unit.Context.Tblusersubmitedanswer.Attach(answer);
                    _unit.Context.Entry(answer).Property(t => t.IsMatched).IsModified = true;
                    _unit.Context.Entry(answer).Property(t => t.LastUpdateDate).IsModified = true;
                    _unit.Context.Entry(answer).Property(t => t.Status).IsModified = true;
                }
            }
            var result = await _unit.SaveChangesAsync();
            if (result.ResultType == ApiResultType.Success)
            {
                return new ApiResultCode(ApiResultType.Success, messageText: "Added");
            }
            return new ApiResultCode(ApiResultType.Error, messageText: "Error");
        }

        public async Task<ApiResult<TestResultQueryModel>> GetTestResultAsync(GetTestResultByTestIdQuery filter)
        {
            try
            {
                TestResultQueryModel modeltest = null;
                var temp = _unit.Context.Tbltestuserright.Where(p => p.UserId == filter.CurrentUserId && p.TestId == filter.Filter.TestId && p.Status == 1)
                    .Include(t => t.User).Include(t => t.Test).ThenInclude(t => t.Tblusersubmitedanswer).ThenInclude(t => t.Question).
                        ThenInclude(t => t.Tblquestionoptions).FirstOrDefault();

                var testresult = await _unit.Context.Tbltest.Where(p => p.Id == filter.Filter.TestId).
                    Include(t => t.Tblusersubmitedanswer).ThenInclude(t => t.Question).
                        ThenInclude(t => t.Tblquestionoptions).Include(t => t.Tbltestuserright).ThenInclude(t => t.User).FirstOrDefaultAsync();

                if (testresult != null)
                {
                    modeltest = new TestResultQueryModel();
                    modeltest.UserId = filter.CurrentUserId.Value;
                    modeltest.UserName = testresult.Tblusersubmitedanswer.Where(t => t.UserId == filter.CurrentUserId).Select(t => (t.User.FirstName + " " + t.User.MiddleName + " " + t.User.LastName)).FirstOrDefault();
                    modeltest.TestId = testresult.Id;
                    modeltest.TestName = testresult.Name;
                    modeltest.TotalQuestion = testresult.NoOfQuestion.HasValue ? testresult.NoOfQuestion.Value : 0;
                    modeltest.TotalAtemptQuestion = testresult.Tblusersubmitedanswer.Where(t => t.UserId == filter.CurrentUserId.Value && t.IsMatched.HasValue).GroupBy(g => g.QuestionId).Count();
                    modeltest.TotalWorngAnswer = testresult.Tblusersubmitedanswer.Where(t => (t.UserId == filter.CurrentUserId.Value && t.IsMatched.HasValue) && t.IsMatched != t.Option.IsMatched).Count();
                    modeltest.TotalCorrenctAnswer = testresult.Tblusersubmitedanswer.Where(t => (t.UserId == filter.CurrentUserId.Value && t.IsMatched.HasValue) && t.IsMatched == t.Option.IsMatched).Count();
                    modeltest.Questions = testresult.Tblusersubmitedanswer.Where(t => t.UserId == filter.CurrentUserId).GroupBy(t => t.QuestionId).Select(q => new TestQuestionQueryModel
                    {
                        Question = q.FirstOrDefault().Question.Name,
                        QuestionId = q.FirstOrDefault().Question.Id,
                        Options = q.FirstOrDefault().Question.Tblusersubmitedanswer.Where(t => t.UserId == filter.CurrentUserId && t.QuestionId == q.FirstOrDefault().Question.Id)
                        .Select(t => new TestQuestionOptionsQueryModel
                        {
                            OptionId = t.Id,
                            Option = t.Option.Name,
                            IsSelect = t.Option.IsMatched,
                            IsCorrect = t.IsMatched
                        }).ToList()
                    }).ToList();
                }
                if (modeltest != null)
                    return new ApiResult<TestResultQueryModel>(new ApiResultCode(ApiResultType.Success), modeltest);

                return new ApiResult<TestResultQueryModel>(new ApiResultCode(ApiResultType.Error, messageText: "no data found"));
            }
            catch (Exception ex)
            {
                ErrorTrace.Logger(LogArea.RepositoryLayer, ex);
                return new ApiResult<TestResultQueryModel>(new ApiResultCode(ApiResultType.Error, messageText: "Error while geting data"));
            }
        }
    }
}
