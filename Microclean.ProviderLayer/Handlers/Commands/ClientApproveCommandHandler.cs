using MediatR;
using Microclean.CommandQueryLayer.Commands;
using Microclean.CommandQueryLayer.Models;
using Microclean.DataModel.Models;
using Microclean.ProviderLayer.Postal;
using Microclean.RepositoryLayer.IRepositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;
using Utilities.ApiRisponse;
using Utilities.ApiRisponse.ResponseUtil;
using Utilities.Logger;
using Utilities.Results;

namespace Microclean.ProviderLayer.Handlers.Commands
{
    public class ClientApproveCommandHandler : BaseRequest,IRequestHandler<ClientApproveCommand, IActionResult>
    {
        private readonly IAccountRepository _account;
        private readonly IEmailSender _emailsend;

        public ClientApproveCommandHandler(IAccountRepository account, IEmailSender emailsend)
        {
            _account = account;
            _emailsend = emailsend;
        }

        public async Task<IActionResult> Handle(ClientApproveCommand request, CancellationToken cancellationToken)
        {
            var _response = new SingleResponse<FranchiseeApprovalResonse>();
            try
            {
                
                var result = await _account.FranchiseeApproveAsync(request);
                if (result.HasSuccess)
                {
                    string msg = string.Empty;
                    if (request.IsApproved)
                    {
                        msg = "Your account has been approved. Please Complete their KYC.";
                    }
                    else
                    {
                        msg = "Your account has been disapproved. Please contact administration";
                    }
                    
                    await _emailsend.SendEmailAsync("ashish.ranjan@pi-infotech.com", "Approval", msg);
                    _response.Status = true;
                    _response.Message = result.ResultCode.MessageText;
                    _response.Data = result.UserObject;
                    return _response.ToHttpResponse();
                }
                else
                {
                    _response.Status = false;
                    _response.Message = result.ResultCode.MessageText;
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
