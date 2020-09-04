using MediatR;
using Microclean.CommandQueryLayer.Models;
using Microclean.CommandQueryLayer.Queries;
using Microsoft.AspNetCore.Mvc;

using Microclean.RepositoryLayer.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Utilities.ApiRisponse.ResponseUtil;
using Microclean.CommandQueryLayer.QueryModels;
using Utilities.ApiRisponse;

namespace Microclean.ProviderLayer.Handlers.Queries
{
    public class GetQuestionCountByCategoryIdQueryHandler : BaseRequest, IRequestHandler<GetQuestionCountQuery, IActionResult>
    {
        private ITestRepository _test;

        public GetQuestionCountByCategoryIdQueryHandler(ITestRepository test)
        {
            _test = test;
        }

        public async Task<IActionResult> Handle(GetQuestionCountQuery request, CancellationToken cancellationToken)
        {
            var _response = new SingleResponse<GetQuestionCountQueryModel>();
            var result = await _test.GetQuestionCountByCategoryIdAsync(request);
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
