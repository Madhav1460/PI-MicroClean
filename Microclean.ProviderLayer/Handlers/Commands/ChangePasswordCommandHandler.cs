using MediatR;
using Microclean.CommandQueryLayer.Commands;
using Microclean.CommandQueryLayer.Models;
using Microclean.DataAccessLayer.Core;
using Microclean.DataModel;
using Microclean.DataModel.Models;
using Microclean.RepositoryLayer.IRepositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Utilities.ApiRisponse;
using Utilities.ApiRisponse.ResponseUtil;
using Utilities.Logger;
using Utilities.Results;

namespace Microclean.ProviderLayer.Handlers.Commands
{
    public class ChangePasswordCommandHandler : BaseRequest, IRequestHandler<ChangePasswordCommand, IActionResult>
    {
        private readonly IAccountRepository _account;

        public ChangePasswordCommandHandler(IAccountRepository account)
        {
            _account = account;
        }

        public async Task<IActionResult> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {

            var _response = new Response();
            try
            {
                var result = await _account.ChangePasswordAsync(request);
                if (result.ResultType == ApiResultType.Success)
                {
                    _response.Status = true;
                    _response.Message = result.MessageText;
                    return _response.ToHttpResponse();
                }
                else
                {
                    _response.Status = false;
                    _response.Message = result.MessageText;
                    return _response.ToHttpResponse();
                }
            }
            catch (Exception ex)
            {
                ErrorTrace.Logger(LogArea.ProviderLayer, ex);
                _response.Status = false;
                _response.Message = "Exception";
                return _response.ToHttpResponse();
            }
        }
    }
}
