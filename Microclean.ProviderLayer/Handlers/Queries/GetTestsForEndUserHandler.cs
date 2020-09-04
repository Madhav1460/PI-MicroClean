using MediatR;
using Microclean.CommandQueryLayer.Models;
using Microclean.CommandQueryLayer.Queries;
using Microclean.CommandQueryLayer.QueryModels;
using Microclean.RepositoryLayer.IRepositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Utilities.ApiRisponse;
using Utilities.ApiRisponse.ResponseUtil;

namespace Microclean.ProviderLayer.Handlers.Queries
{
    public class GetTestsForEndUserHandler : BaseRequest, IRequestHandler<GetTestsForEndUser, IActionResult>
    {
        private readonly ITestForEndUserRepository _testForEnd;

        public GetTestsForEndUserHandler(ITestForEndUserRepository testForEnd)
        {
            _testForEnd = testForEnd;
        }
        public async Task<IActionResult> Handle(GetTestsForEndUser request, CancellationToken cancellationToken)
        {
            var _response = new ListResponse<TestListQueryModel>();
            var result = await _testForEnd.GetTestListync(request);
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
