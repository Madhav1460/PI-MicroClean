using MediatR;
using Microclean.CommandQueryLayer.Models;
using Microclean.CommandQueryLayer.Queries;
using Microclean.CommandQueryLayer.QueryModels;
using Microclean.RepositoryLayer.IRepositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using Utilities.ApiRisponse;
using Utilities.ApiRisponse.ResponseUtil;

namespace Microclean.ProviderLayer.Handlers.Queries
{
    public class GetTestQuestionOptionForEndUserByTestIdHandler : BaseRequest, IRequestHandler<GetTestQuestionOptionForEndUserByTestId, IActionResult>
    {
        private readonly ITestForEndUserRepository _testForEnd;

        public GetTestQuestionOptionForEndUserByTestIdHandler(ITestForEndUserRepository testForEnd)
        {
            _testForEnd = testForEnd;
        }

        public async Task<IActionResult> Handle(GetTestQuestionOptionForEndUserByTestId request, CancellationToken cancellationToken)
        {
            var _response = new SingleResponse<TestQueryModel>();
            var result = await _testForEnd.GetTestQuestionOptionAsync(request);
            if (result.HasSuccess)
            {
                _response.Data = result.UserObject;
                _response.Status = true;
            }
            else
            {
                _response.Message = result.ResultCode.MessageText;
                _response.Status = true;
            }
            return _response.ToHttpResponse();
        }
    }
}
