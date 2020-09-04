using MediatR;
using Microclean.CommandQueryLayer.Queries;
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
    public class GetAllAdminEmployeeQueryHandler : IRequestHandler<GetAllAdminEmployeeQuery, IActionResult>
    {
        private readonly ICompanyRepository _company;

        public GetAllAdminEmployeeQueryHandler(ICompanyRepository company)
        {
            _company = company;
        }

        public async Task<IActionResult> Handle(GetAllAdminEmployeeQuery request, CancellationToken cancellationToken)
        {
            var _response = new ListResponse<GetAllAdminEmployeeQuery>();
            var result = await _company.GetAllAdminEmployee();
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
