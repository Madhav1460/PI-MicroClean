using MediatR;
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
    class ExistingTestNameCheckQueryHandler : IRequestHandler<ExistingTestNameCheckQuery, IActionResult>
    {
        private readonly IFranchiseeRepository _franchisee;

        public ExistingTestNameCheckQueryHandler(IFranchiseeRepository franchisee)
        {
            _franchisee = franchisee;
        }

        public async Task<IActionResult> Handle(ExistingTestNameCheckQuery request, CancellationToken cancellationToken)
        {
            var _response = new Response();
            var result = await _franchisee.IsTestNameExist(request.TestName);
            if (result.UserObject)
            {
                _response.Status = false;
                _response.Message = "Test Name already in use.";
                _response.ErrorTypeCode = (int)ErrorMessage.Email;
            }
            return _response.ToHttpResponse();
        }
    }
}
