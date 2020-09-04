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
    public class CommercialFeeQueryByFranchiseeIdHandler : BaseRequest,IRequestHandler<GetCommercialFeeQueryByFranchiseeId, IActionResult>
    {
        private readonly IPaymentRepository _paymnet;

        public CommercialFeeQueryByFranchiseeIdHandler(IPaymentRepository paymnet)
        {
            _paymnet = paymnet;
        }

        public async Task<IActionResult> Handle(GetCommercialFeeQueryByFranchiseeId request, CancellationToken cancellationToken)
        {
            var _response = new SingleResponse<FranchiseeFeeDetail>();
            var result = await _paymnet.GetCommercialFeeByUserId(request.UserId);
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
