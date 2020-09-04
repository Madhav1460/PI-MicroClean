using Microclean.CommandQueryLayer.Commands;
using Microclean.CommandQueryLayer.Filter;
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
using System.Text;
using System.Threading.Tasks;
using Utilities.Logger;
using Utilities.Results;

namespace Microclean.RepositoryLayer.Repositories
{
    public class TestRepository : ITestRepository
    {
        private readonly IUnitOfWork<MicrocleanDbContext> _unit;

        public TestRepository(IUnitOfWork<MicrocleanDbContext> unit)
        {
            _unit = unit;
        }

        public async Task<ApiResultCode> CreateTestAsync(CreateTestCommand request)
        {
            try
            {
                string filter = _unit.Context.Tbltest.Where(t => t.CategoryId == request.CatetoryId).Select(t => t.Name).FirstOrDefault();
                var modeldto = new Tbltest();
                modeldto.Name = request.Name;
                modeldto.Description = request.Description;
                modeldto.NoOfQuestion = request.NoOfQuestion;
                modeldto.CategoryId = request.CatetoryId;
                modeldto.InsertDate = DateTime.Now;
                modeldto.LastUpdateDate = DateTime.Now;
                modeldto.Status = 1;
                modeldto.InsertedBy = request.CurrentUserId;
                modeldto.LastUpdatedBy = request.CurrentUserId;
                modeldto.ClientId = request.CurrentCientId;
                if (request.Id > 0)
                {
                    modeldto.Id = request.Id;
                    _unit.Context.Tbltest.Attach(modeldto);
                    _unit.Context.Entry(modeldto).Property(t => t.Name).IsModified = true;
                    _unit.Context.Entry(modeldto).Property(t => t.Description).IsModified = true;
                    _unit.Context.Entry(modeldto).Property(t => t.NoOfQuestion).IsModified = true;
                    _unit.Context.Entry(modeldto).Property(t => t.CategoryId).IsModified = true;
                    _unit.Context.Entry(modeldto).Property(t => t.LastUpdateDate).IsModified = true;
                    _unit.Context.Entry(modeldto).Property(t => t.LastUpdatedBy).IsModified = true;
                }
                else
                {
                    if (filter != null && filter.ToLower() == request.Name.ToLower())
                    {
                        return new ApiResultCode(ApiResultType.Error, messageText: " Duplicate Test can not be Allowed");
                    }
                    _ = _unit.GetRepository<Tbltest>().Add(modeldto);
                }
                var result = await _unit.SaveChangesAsync();
                if (result.ResultType == ApiResultType.Success)
                {
                    return new ApiResultCode(ApiResultType.Success, messageText: "Added");
                }
                return new ApiResultCode(ApiResultType.Error, messageText: " Can not be Added");
            }
            catch (Exception ex)
            {
                ErrorTrace.Logger(LogArea.RepositoryLayer, ex);
                return new ApiResultCode(ApiResultType.Error, messageText: " Can not be Added");
            }
        }

        public async Task<ApiResultCode> DeleteTestAsync(DeleteTestCommand datamodel)
        {
            if (_unit.Context.Tblusersubmitedanswer.Where(t => t.TestId == datamodel.Id).Any())
                return new ApiResultCode(ApiResultType.Error, messageText: "Exist test will not delete from record. user have already tacken test!");

            var tbltest = new Tbltest();
            tbltest.Id = datamodel.Id;
            tbltest.Status = 0;
            tbltest.LastUpdatedBy = datamodel.CurrentUserId;
            tbltest.LastUpdateDate = DateTime.Now;

            _unit.Context.Tbltest.Attach(tbltest);
            _unit.Context.Entry(tbltest).Property(t => t.Status).IsModified = true;
            _unit.Context.Entry(tbltest).Property(t => t.LastUpdatedBy).IsModified = true;
            _unit.Context.Entry(tbltest).Property(t => t.LastUpdateDate).IsModified = true;


            var result = await _unit.SaveChangesAsync();
            if (result.ResultType == ApiResultType.Success)
            {
                return new ApiResultCode(ApiResultType.Success, messageText: "Test Deleted!");
            }
            return new ApiResultCode(ApiResultType.Error, messageText: "Can not Delete!");
        }

        public async Task<ApiResult<IEnumerable<UserTestMappingResponseModel>>> GetAllTestUser(GetAllTestUserQuery request)
        {
            try
            {
                List<UserTestMappingResponseModel> result = new List<UserTestMappingResponseModel>();
                var query = await _unit.Context.EmployeeDataModel.FromSqlRaw("WITH user_CTE AS (SELECT tsr.Id AS Id, CONCAT(r.FirstName,' ',IFNULL(r.MiddleName,''),' ',IFNULL(r.LastName,'')) AS FullName,r.Id AS UserId FROM tbluser r INNER JOIN `tbltestuserright` tsr ON r.Id=tsr.UserId INNER JOIN `tbltest` tt ON tt.Id = tsr.TestId WHERE tsr.TestId={0} AND tsr.Status=1)SELECT cte.Id AS TestId,CONCAT(u.FirstName,' ',IFNULL(u.MiddleName,''),' ',IFNULL(u.LastName,'')) AS FullName,u.Id AS UserId FROM tbluser u LEFT JOIN user_CTE cte ON u.Id=cte.userId WHERE u.UserTypeId=3 && u.ClientId={1} ORDER BY u.FirstName", request.Filter.TestId,request.CurrentCientId).ToListAsync();
                
                foreach (var item in query)
                {
                    UserTestMappingResponseModel model = new UserTestMappingResponseModel();
                    model.FullName = item.FullName;
                    model.TestId = item.TestId;
                    model.UserId = item.UserId.Value;
                    result.Add(model);
                }
                if (result != null && result.Count > 0)
                {
                    return new ApiResult<IEnumerable<UserTestMappingResponseModel>>(new ApiResultCode(ApiResultType.Success), result);
                }
            }
            catch (Exception ex)
            {
                ErrorTrace.Logger(LogArea.RepositoryLayer, ex);
                return new ApiResult<IEnumerable<UserTestMappingResponseModel>>(new ApiResultCode(ApiResultType.Error, messageText: "Data not found"));
            }
            return new ApiResult<IEnumerable<UserTestMappingResponseModel>>(new ApiResultCode(ApiResultType.Error, messageText: "Data not found"));
        }
    

        public async Task<ApiResult<GetQuestionCountQueryModel>> GetQuestionCountByCategoryIdAsync(GetQuestionCountQuery filter)
        {
            try
            {
                GetQuestionCountQueryModel result = new GetQuestionCountQueryModel();
                var query = await _unit.Context.Tblquestion.Where(t => (filter.Filter.CategoryId.HasValue ? t.CategoryId == filter.Filter.CategoryId.Value : true) && t.Status == 1).CountAsync();
                if (query > 0)
                    result.Count = query;

                return new ApiResult<GetQuestionCountQueryModel>(new ApiResultCode(ApiResultType.Success), result);
            }
            catch (Exception ex)
            {
                ErrorTrace.Logger(LogArea.RepositoryLayer, ex);
                return new ApiResult<GetQuestionCountQueryModel>(new ApiResultCode(ApiResultType.Error, messageText: "Error while geting data"));
            }
        }

        public async Task<ApiResult<IList<TestsQueryModel>>> GetTestAsync(GetTestByCategoryId filter)
        {
            try
            {
                if (filter.CurrentRoleId != 2)
                    return new ApiResult<IList<TestsQueryModel>>(new ApiResultCode(ApiResultType.Error, messageText: "You have no access to get this data."));

                var result = await _unit.GetRepository<Tbltest>().GetSelectedDataAsync(t => (filter.Filter.CategoryId.HasValue ? t.CategoryId == filter.Filter.CategoryId : true) && t.ClientId == filter.CurrentCientId && t.Status == 1, s => new TestsQueryModel
                {
                    Id = s.Id,
                    Name = s.Name
                });
                if (result != null && result.HasSuccess)
                {
                    return new ApiResult<IList<TestsQueryModel>>(new ApiResultCode(ApiResultType.Success), result.UserObject.ToList());
                }
            }
            catch (Exception ex)
            {
                ErrorTrace.Logger(LogArea.RepositoryLayer, ex);
                return new ApiResult<IList<TestsQueryModel>>(new ApiResultCode(ApiResultType.Error, messageText: "Error while geting data"));
            }
            return new ApiResult<IList<TestsQueryModel>>(new ApiResultCode(ApiResultType.Error, messageText: "no data found"));
        }

        public async Task<ApiResult<CreateTestCommand>> GetTestByIdAsync(long id)
        {
            try
            {
                if (_unit.Context.Tblusersubmitedanswer.Where(t => t.TestId == id).Any())
                return new ApiResult<CreateTestCommand>(new ApiResultCode(ApiResultType.Error, messageText: "Exist edit will not delete from record. user have already tacken test."));

                var result = await _unit.GetRepository<Tbltest>().GetByID(id);
                if (result.HasSuccess)
                {
                    var testview = new CreateTestCommand();
                    testview.Id = result.UserObject.Id;
                    testview.CatetoryId = result.UserObject.CategoryId;
                    testview.Name = result.UserObject.Name;
                    testview.Description = result.UserObject.Description;
                    testview.NoOfQuestion = result.UserObject.NoOfQuestion.HasValue ? result.UserObject.NoOfQuestion.Value : 0;
                    return new ApiResult<CreateTestCommand>(new ApiResultCode(ApiResultType.Success), testview);
                }
            }
            catch (Exception ex)
            {
                ErrorTrace.Logger(LogArea.RepositoryLayer, ex);
                return new ApiResult<CreateTestCommand>(new ApiResultCode(ApiResultType.Error, messageText: "Error while geting data"));
            }
            return new ApiResult<CreateTestCommand>(new ApiResultCode(ApiResultType.Error, messageText: "no data found"));
        }

        public async Task<ApiResultCode> MappingUserAsignTestAsync(UserTestMappingCommand request)
        {

            long userId = 0;
            try
            {
                Tbltestuserright userTest = null;
                foreach (var item in request.SelectedUsers)
                {
                    if (item.UserTestId.HasValue && item.UserTestId > 0)
                    {
                        userTest = new Tbltestuserright();
                        userTest.Id = item.UserTestId.Value;
                        userTest.LastUpdateDate = DateTime.Now;
                        userTest.Status = 0;
                        _unit.Context.Tbltestuserright.Attach(userTest);
                        _unit.Context.Entry(userTest).Property(m => m.Status).IsModified = true;
                    }
                    else
                    {
                        userTest = new Tbltestuserright();
                        userTest.InsertDate = DateTime.Now;
                        userTest.IsApproved = 1;
                        userTest.Status = 1;
                        userTest.UserId = item.UserId;
                        userTest.TestId = request.TestId;
                        _ = await _unit.GetRepository<Tbltestuserright>().Add(userTest);
                    }
                }
                var result = _ = await _unit.SaveChangesAsync();
                if (result.ResultType == ApiResultType.Success)
                {
                    userId = userTest.Id; return new ApiResultCode(ApiResultType.Success, messageText: "Added");
                }
            }
            catch (Exception ex)
            {
                ErrorTrace.Logger(LogArea.RepositoryLayer, ex); return new ApiResultCode(ApiResultType.Error, messageText: " Can not be Added");
            }
            return new ApiResultCode(ApiResultType.Error, messageText: " Can not be Added");


        }
    }
}
