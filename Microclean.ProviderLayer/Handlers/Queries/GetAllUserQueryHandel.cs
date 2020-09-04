using MediatR;
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
    public class GetAllUserQueryHandel : BaseRequest, IRequestHandler<GetAllUserQuery, IActionResult>
    {
        private readonly IAccountRepository _account;
        public GetAllUserQueryHandel(IAccountRepository account)
        {
            _account = account;
        }
        public async Task<IActionResult> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
        {
            var _response = new ListResponse<UserResponseQueryModel>();
            //var result = await _account.GetAllUserForFranchisee(request.CurrentUserId.Value);
            var result = await _account.GetAllUserForFranchisee(request);
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
