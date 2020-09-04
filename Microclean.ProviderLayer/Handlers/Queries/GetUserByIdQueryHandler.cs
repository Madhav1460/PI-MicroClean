using MediatR;
using Microclean.CommandQueryLayer.Models;
using Microclean.CommandQueryLayer.Queries;
using Microclean.CommandQueryLayer.QueryModels;
using Microclean.RepositoryLayer.IRepositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using Utilities.ApiRisponse;
using Utilities.ApiRisponse.ResponseUtil;

namespace Microclean.ProviderLayer.Handlers.Queries
{
    public class GetUserByIdQueryHandler : BaseRequest,IRequestHandler<GetUserByIdQuery, IActionResult>
    {
        private readonly IFranchiseeRepository _franchisee;

        public GetUserByIdQueryHandler(IFranchiseeRepository franchisee)
        {
            _franchisee = franchisee;
        }

        public async Task<IActionResult> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var _response = new SingleResponse<CompanyDetailQueryModel>();
            
                request.UserId = request.UserId > 0 ? request.UserId : request.CurrentUserId.Value;

            var result = await _franchisee.GetUserDetailByUserIdAsync(request.UserId);
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
