using MediatR;
using Microclean.CommandQueryLayer.Commands;
using Microclean.CommandQueryLayer.Models;
using Microclean.RepositoryLayer.IRepositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Utilities.ApiRisponse;
using Utilities.ApiRisponse.ResponseUtil;

namespace Microclean.ProviderLayer.Handlers.Commands
{
    public class UserTestMappingCommandHandler : BaseRequest, IRequestHandler<UserTestMappingCommand, IActionResult>
    {
        private ITestRepository _test;

        public UserTestMappingCommandHandler(ITestRepository test)
        {
            _test = test;
        }

        public async Task<IActionResult> Handle(UserTestMappingCommand request, CancellationToken cancellationToken)
        {
            var _response = new Response();
            var result = await _test.MappingUserAsignTestAsync(request);
            if (result.ResultType == Utilities.Results.ApiResultType.Success)
            {
                _response.Message = result.MessageText;
                _response.Status = true;
            }
            else
            {
                _response.Message = result.MessageText;
                _response.Status = false;
            }
            return _response.ToHttpResponse();
        }
    }
}
