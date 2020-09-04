using Microclean.CommandQueryLayer.Commands;
using Microclean.CommandQueryLayer.Queries;
using Microclean.CommandQueryLayer.QueryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Results;

namespace Microclean.RepositoryLayer.IRepositories
{
    public interface ITestForEndUserRepository
    {
        Task<ApiResult<TestQueryModel>> GetTestQuestionOptionAsync(GetTestQuestionOptionForEndUserByTestId filter);
        Task<ApiResult<IEnumerable<TestListQueryModel>>> GetTestListync(GetTestsForEndUser filter);
        Task<ApiResultCode> SubmitTestAsync(SubmitTestCommand command);
        Task<ApiResult<TestResultQueryModel>> GetTestResultAsync(GetTestResultByTestIdQuery filter);
    }
}
