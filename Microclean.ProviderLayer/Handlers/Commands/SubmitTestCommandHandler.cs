using MediatR;
using Microclean.CommandQueryLayer.Commands;
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
using Utilities.Results;

namespace Microclean.ProviderLayer.Handlers.Commands
{
    public class SubmitTestCommandHandler : IRequestHandler<SubmitTestCommand, IActionResult>
    {
        private readonly ITestForEndUserRepository _testForEndUser;

        public SubmitTestCommandHandler(ITestForEndUserRepository testForEndUser)
        {
            _testForEndUser = testForEndUser;
        }

        public async Task<IActionResult> Handle(SubmitTestCommand request, CancellationToken cancellationToken)
        {
            var _response = new Response();
            var result = await _testForEndUser.SubmitTestAsync(request);
            if (result.ResultType == ApiResultType.Success)
            {
                _response.Status = true;
                _response.Message = result.MessageText;
            }
            else
            {
                _response.Status = false;
                _response.Message = result.MessageText;
            }
            return _response.ToHttpResponse();
        }
    }
}
