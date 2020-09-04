using Microclean.CommandQueryLayer.Commands;
using Microclean.CommandQueryLayer.Models;
using Microclean.CommandQueryLayer.Queries;
using Microclean.CommandQueryLayer.QueryModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utilities.Results;

namespace Microclean.RepositoryLayer.IRepositories
{
    public interface ITestRepository
    {
        Task<ApiResultCode> CreateTestAsync(CreateTestCommand model);
        Task<ApiResult<IList<TestsQueryModel>>> GetTestAsync(GetTestByCategoryId filter);
        Task<ApiResult<CreateTestCommand>> GetTestByIdAsync(long id);
        Task<ApiResult<GetQuestionCountQueryModel>> GetQuestionCountByCategoryIdAsync(GetQuestionCountQuery filter);
        Task<ApiResultCode> DeleteTestAsync(DeleteTestCommand datamodel);
        Task<ApiResultCode> MappingUserAsignTestAsync(UserTestMappingCommand request);
        Task<ApiResult<IEnumerable<UserTestMappingResponseModel>>> GetAllTestUser(GetAllTestUserQuery request);
        //Task<ApiResult<IList<TestWIthQuestionListQuery>>> GetTestWithQuestionCount(GetQuestionFilterQuery filter);
    }
}
