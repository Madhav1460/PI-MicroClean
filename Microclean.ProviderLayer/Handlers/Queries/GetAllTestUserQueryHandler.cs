using MediatR;
using Microclean.CommandQueryLayer.Models;
using Microclean.CommandQueryLayer.Queries;
using Microclean.RepositoryLayer.IRepositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Utilities.ApiRisponse;
using Utilities.ApiRisponse.ResponseUtil;

namespace Microclean.ProviderLayer.Handlers.Queries
{
    public class GetAllTestUserQueryHandler : BaseRequest, IRequestHandler<GetAllTestUserQuery, IActionResult>
    {
        private readonly ITestRepository _test;

        public GetAllTestUserQueryHandler(ITestRepository test)
        {
            _test = test;
        }

        public async Task<IActionResult> Handle(GetAllTestUserQuery request, CancellationToken cancellationToken)
        {
            var _response = new ListResponse<UserTestMappingResponseModel>();
            var result = await _test.GetAllTestUser(request);
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
