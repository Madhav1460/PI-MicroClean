using MediatR;
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
    public class GetAllDistrictByStateIdQueryHandler : IRequestHandler<GetAllDistrictByStateIdQuery, IActionResult>
    {
        private readonly ICommonRepository _common;

        public GetAllDistrictByStateIdQueryHandler(ICommonRepository common)
        {
            _common = common;
        }

        public async Task<IActionResult> Handle(GetAllDistrictByStateIdQuery request, CancellationToken cancellationToken)
        {
            var _response = new ListResponse<DistrictQueryMode>();
            var result = await _common.GetAllDistrictByStateId(request.StateId);
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
