using MediatR;
using Microclean.CommandQueryLayer.Queries;
using Microclean.CommandQueryLayer.QueryModels;
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
    public class GetAllCategoryQueryHandler : IRequestHandler<GetAllCategoryQuery, IActionResult>
    {
        private readonly ICommonRepository _common;

        public GetAllCategoryQueryHandler(ICommonRepository common)
        {
            _common = common;
        }

        public async Task<IActionResult> Handle(GetAllCategoryQuery request, CancellationToken cancellationToken)
        {
            var _response = new ListResponse<GetAllCategoryQueryModel>();
            var result = await _common.GetAllCategory();
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
