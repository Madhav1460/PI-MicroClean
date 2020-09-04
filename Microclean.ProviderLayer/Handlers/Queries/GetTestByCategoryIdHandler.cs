using MediatR;
using Microclean.CommandQueryLayer.Commands;
using Microclean.CommandQueryLayer.Models;
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
    public class GetTestByCategoryIdHandler : BaseRequest, IRequestHandler<GetTestByCategoryId, IActionResult>
    {
        private ITestRepository _test;

        public GetTestByCategoryIdHandler(ITestRepository test)
        {
            _test = test;
        }

        public async Task<IActionResult> Handle(GetTestByCategoryId request, CancellationToken cancellationToken)
        {
            var _response = new ListResponse<TestsQueryModel>();
            dynamic result = "";
            result = await _test.GetTestAsync(request);

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
