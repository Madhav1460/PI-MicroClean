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
    class ExistingPhoneCheckQueryHandler : IRequestHandler<ExistingPhoneCheckQuery, IActionResult>
    {
        private readonly IAccountRepository _account;

        public ExistingPhoneCheckQueryHandler(IAccountRepository account)
        {
            _account = account;
        }

        public async Task<IActionResult> Handle(ExistingPhoneCheckQuery request, CancellationToken cancellationToken)
        {
            var _response = new Response();
            var result = await _account.IsPhoneExist(request.Phone);
            if (result.UserObject)
            {
                _response.Status = false;
                _response.Message = "Phone no. already in use.";
                _response.ErrorTypeCode = (int)ErrorMessage.Phone;
                return _response.ToHttpResponse();
            }
            return _response.ToHttpResponse();
        }
    }
}
