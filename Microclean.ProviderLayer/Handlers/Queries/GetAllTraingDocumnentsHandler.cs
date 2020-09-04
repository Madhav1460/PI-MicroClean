using MediatR;
using Microclean.CommandQueryLayer.Models;
using Microclean.CommandQueryLayer.Queries;
using Microclean.RepositoryLayer.IRepositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using Utilities.ApiRisponse;
using Utilities.ApiRisponse.ResponseUtil;

namespace Microclean.ProviderLayer.Handlers.Queries
{
    public class GetAllTraingDocumnentsHandler : IRequestHandler<GetAllTraingDocumnentsQuery, IActionResult>
    {
        private readonly IUserTranninRepository _usertraing;
        public GetAllTraingDocumnentsHandler(IUserTranninRepository usertraing)
        {
            _usertraing = usertraing;
        }

        public async Task<IActionResult> Handle(GetAllTraingDocumnentsQuery request, CancellationToken cancellationToken)
        {
            var _response = new ListResponse<TraingDocumnentsResponseModel>();
            var result = await _usertraing.GetAllTrainingDocuments();
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
