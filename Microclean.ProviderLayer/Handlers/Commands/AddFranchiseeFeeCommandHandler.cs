using MediatR;
using Microclean.CommandQueryLayer.Commands;
using Microclean.CommandQueryLayer.Models;
using Microclean.DataModel.Models;
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
using Utilities.Logger;

namespace Microclean.ProviderLayer.Handlers.Commands
{
    public class AddFranchiseeFeeCommandHandler : BaseRequest, IRequestHandler<AddFranchiseeFeeCommand, IActionResult>
    {
        private readonly IPaymentRepository _payment;

        public AddFranchiseeFeeCommandHandler(IPaymentRepository payment)
        {
            _payment = payment;
        }

        public async Task<IActionResult> Handle(AddFranchiseeFeeCommand request, CancellationToken cancellationToken)
        {
            var _response = new Response();
            var tblfeedetail = new List<Tblfeedetail>();
            try
            {

                tblfeedetail.Add(new Tblfeedetail
                {
                    Id = request.FranchiseeFeesId,
                    UserId = request.UserId,
                    FeeValue = Convert.ToDecimal(request.FranchiseeFee),
                    FeeTypeId = request.FranchiseeFeeId,
                    TotalFee = request.TotalAmmount,
                    PaidAmmout = Convert.ToDecimal(request.FranchiseeFeePaidAmout),
                    PaymentTerms = request.PaymentTerms,
                    PaymentDueDate = Convert.ToDateTime(request.PaymentDueDate, System.Globalization.CultureInfo.GetCultureInfo("hi-IN").DateTimeFormat)
                });
                tblfeedetail.Add(new Tblfeedetail
                {
                    Id = request.OtherFeesId,
                    UserId = request.UserId,
                    FeeValue = Convert.ToDecimal(request.OtherFee),
                    FeeTypeId = request.OtherFeeId,
                    TotalFee = request.TotalAmmount,
                    PaidAmmout = Convert.ToDecimal(request.OtherFeePaidAmout),
                    PaymentTerms = request.PaymentTerms,
                    PaymentDueDate = Convert.ToDateTime(request.PaymentDueDate, System.Globalization.CultureInfo.GetCultureInfo("hi-IN").DateTimeFormat)
                });
                tblfeedetail.Add(new Tblfeedetail
                {
                    Id = request.FixedMonthlyFeesId,
                    UserId = request.UserId,
                    FeeValue = Convert.ToDecimal(request.FixedMonthlyFee),
                    FeeTypeId = request.FixedMonthlyFeeId,
                    TotalFee = request.TotalAmmount,
                    PaidAmmout = Convert.ToDecimal(request.FixedMonthlyFeePaidAmout),
                    PaymentTerms = request.PaymentTerms,
                    PaymentDueDate = Convert.ToDateTime(request.PaymentDueDate, System.Globalization.CultureInfo.GetCultureInfo("hi-IN").DateTimeFormat)
                });
                tblfeedetail.Add(new Tblfeedetail
                {
                    Id = request.ThresholdAmountsId,
                    UserId = request.UserId,
                    FeeValue = Convert.ToDecimal(request.ThresholdAmount),
                    FeeTypeId = request.ThresholdAmountId,
                    TotalFee = request.TotalAmmount,
                    PaidAmmout = Convert.ToDecimal(request.ThresholdAmountPaidAmount),
                    PaymentTerms = request.PaymentTerms,
                    PaymentDueDate = Convert.ToDateTime(request.PaymentDueDate, System.Globalization.CultureInfo.GetCultureInfo("hi-IN").DateTimeFormat),
                    UpdatedBy = request.CurrentUserId,
                    Updatedate = DateTime.Now
                });
                var result = await _payment.AddUpdateFee(tblfeedetail, request.PaymentTypeId, request.PaymentRef);
                if (result.ResultType == Utilities.Results.ApiResultType.Success)
                {
                    _response.Status = true;
                    _response.Message = result.MessageText;
                    return _response.ToHttpResponse();
                }
                _response.Status = false;
                _response.Message = result.MessageText;
                return _response.ToHttpResponse();
            }
            catch (Exception ex)
            {
                ErrorTrace.Logger(LogArea.ProviderLayer, ex);
                _response.Status = false;
                _response.Message = "Exception";
            }
            return _response.ToHttpResponse();
        }
    }
}
