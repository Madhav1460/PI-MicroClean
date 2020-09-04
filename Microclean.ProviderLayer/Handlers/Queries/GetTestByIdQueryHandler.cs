using MediatR;
using Microclean.CommandQueryLayer.Commands;
using Microclean.CommandQueryLayer.Models;
using Microclean.CommandQueryLayer.Queries;
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
    public class GetTestByIdQueryHandler : IRequestHandler<GetTestByIdQuery, IActionResult>
    {
        private ITestRepository _test;

        public GetTestByIdQueryHandler(ITestRepository test)
        {
            _test = test;
        }

        public async Task<IActionResult> Handle(GetTestByIdQuery request, CancellationToken cancellationToken)
        {
            var _response = new SingleResponse<CreateTestCommand>();
            var result = await _test.GetTestByIdAsync(request.TestId);
            if (result.HasSuccess)
            {
                _response.Data = result.UserObject;
                _response.Status = true;
            }
            else
            {
                _response.Message = result.ResultCode.MessageText;
                _response.Status = false;
            }
            return _response.ToHttpResponse();
        }
    }
}
