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
    class ExistingEmailCheckQueryHandler : IRequestHandler<ExistingEmailCheckQuery, IActionResult>
    {
        private readonly IAccountRepository _account;

        public ExistingEmailCheckQueryHandler(IAccountRepository account)
        {
            _account = account;
        }

        public async Task<IActionResult> Handle(ExistingEmailCheckQuery request, CancellationToken cancellationToken)
        {
            var _response = new Response();
            var result = await _account.IsEmailExist(request.Email);
            if (result.UserObject)
            {
                _response.Status = false;
                _response.Message = "Email already in use.";
                _response.ErrorTypeCode = (int)ErrorMessage.Email;
            }
            return _response.ToHttpResponse();
        }
    }
}
