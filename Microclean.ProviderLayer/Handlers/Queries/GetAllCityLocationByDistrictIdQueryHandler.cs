using MediatR;
using Microclean.CommandQueryLayer.Queries;
using Microclean.CommandQueryLayer.QueryModels;
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
    public class GetAllCityLocationByDistrictIdQueryHandler : IRequestHandler<GetAllCityLocationByDistrictIdQuery, IActionResult>
    {
        private readonly ICommonRepository _common;
        public GetAllCityLocationByDistrictIdQueryHandler(ICommonRepository common)
        {
            _common = common;
        }
        public async Task<IActionResult> Handle(GetAllCityLocationByDistrictIdQuery request, CancellationToken cancellationToken)
        {
            var _response = new ListResponse<CityLocationQueryMode>();
            var result = await _common.GetAllCityLocationByDistrictId(request.DistrictId);
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
