﻿using MediatR;
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
    public class GetAllStateByCountryIdHandler : IRequestHandler<GetAllStateByCountryId, IActionResult>
    {
        private readonly ICommonRepository _common;
        public GetAllStateByCountryIdHandler(ICommonRepository common)
        {
            _common = common;
        }
        public async Task<IActionResult> Handle(GetAllStateByCountryId request, CancellationToken cancellationToken)
        {
            var _response = new ListResponse<StateQueryModel>();
            var result = await _common.GetAllState(request.CountryId);
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
