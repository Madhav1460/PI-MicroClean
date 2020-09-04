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
using Utilities.ApiRisponse.IResponseUtil;
using Utilities.ApiRisponse.ResponseUtil;

namespace Microclean.ProviderLayer.Handlers.Queries
{
    public class GetPaymentTypeQueryHandler : IRequestHandler<GetPaymentTypeQuery, IActionResult>
    {
        private readonly IPaymentTypeRepository _payment;

        public GetPaymentTypeQueryHandler(IPaymentTypeRepository payment)
        {
            _payment = payment;
        }

        public async Task<IActionResult> Handle(GetPaymentTypeQuery request, CancellationToken cancellationToken)
        {
            var _response = new ListResponse<PaymentTypeQueryModel>();
            var result = await _payment.GetAllPaymentType();
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
