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
    public class GetAllDocumentTypeQueryHandler : IRequestHandler<GetAllDocumentTypeQuery, IActionResult>
    {
        private readonly IAccountRepository _account;
        private UserClameResponse _userinfo;

        public GetAllDocumentTypeQueryHandler(IAccountRepository account, UserClameResponse userinfo)
        {
            _account = account;
            _userinfo = userinfo;
        }

        public async Task<IActionResult> Handle(GetAllDocumentTypeQuery request, CancellationToken cancellationToken)
        {
            var _response = new ListResponse<DocumentTypeQueryModel>();
            var result = await _account.GetAllDocumentTypeAsync();
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
