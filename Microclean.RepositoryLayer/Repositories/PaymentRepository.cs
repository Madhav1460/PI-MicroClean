using Microclean.CommandQueryLayer.QueryModels;
using Microclean.DataAccessLayer.Core;
using Microclean.DataModel;
using Microclean.DataModel.Models;
using Microclean.RepositoryLayer.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Logger;
using Utilities.Results;

namespace Microclean.RepositoryLayer.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly IUnitOfWork<MicrocleanDbContext> _unit;

        public PaymentRepository(IUnitOfWork<MicrocleanDbContext> unit)
        {
            _unit = unit;
        }

        public async Task<ApiResultCode> AddUpdateFee(List<Tblfeedetail> datamodel, short? paymenttypeid = null, string paymnetref = null)
        {
            List<Tblfranchiseepayments> duepayments = new List<Tblfranchiseepayments>();
            if (datamodel.Any(t => t.Id != 0))
            {
                foreach (var item in datamodel)
                {
                    var data = datamodel.Where(t => t.FeeValue > 0 && t.TotalFee > 0 && t.PaidAmmout > 0);
                    if (item.Id > 0)
                    {
                        _unit.Context.Tblfeedetail.Attach(item);
                        _unit.Context.Entry(item).Property(t => t.PaymentTerms).IsModified = true;
                        _unit.Context.Entry(item).Property(t => t.PaymentDueDate).IsModified = true;
                        if (item.TotalFee > 0)
                            _unit.Context.Entry(item).Property(t => t.TotalFee).IsModified = true;

                        if (item.FeeValue > 0)
                            _unit.Context.Entry(item).Property(t => t.FeeValue).IsModified = true;

                        if (item.PaidAmmout > 0)
                            _unit.Context.Entry(item).Property(t => t.PaidAmmout).IsModified = true;
                    }
                    if (item.Id > 0 && item.PaidAmmout > 0 && paymenttypeid.HasValue)
                    {
                        Tblfranchiseepayments duepayment = new Tblfranchiseepayments();
                        duepayment.FeeDetailId = item.Id;
                        duepayment.PaymentTypeId = paymenttypeid;
                        duepayment.PaymentDate = item.PaymentDueDate;
                        duepayment.RecievedPayment = item.PaidAmmout;
                        duepayment.PaymentReference = paymnetref;
                        duepayment.TotalAmountPaid = item.TotalFee;
                        duepayments.Add(duepayment);
                    }
                }
            }
            else
            {
                _ = _unit.GetRepository<Tblfeedetail>().AddAll(datamodel);
            }
            if (duepayments.Count > 0)
                _ = _unit.GetRepository<Tblfranchiseepayments>().AddAll(duepayments);

            var result = await _unit.SaveChangesAsync();

            if (result.ResultType == ApiResultType.Success)
                return new ApiResultCode(ApiResultType.Success, messageText: "Fee Added Successfully");

            return new ApiResultCode(ApiResultType.Error, messageText: "Error during create");
        }

        public async Task<ApiResult<FranchiseeFeeDetail>> GetCommercialFeeByUserId(long userId)
        {
            try
            {
                FranchiseeFeeDetail feeDetail = new FranchiseeFeeDetail();
                var query = _unit.Context.Tblfeedetail.Include(t => t.Tblfranchiseepayments).ThenInclude(t => t.PaymentType).Where(t => t.UserId == userId).AsQueryable();
                var result = await query.Select(
                     t => new CommercialFeeQueryMode
                     {
                         FeeId = t.Id,
                         FeeValue = t.FeeValue.HasValue ? t.FeeValue.Value : 0,
                         FeeTypeId = t.FeeTypeId.Value,
                         PaymentDue = (t.Tblfranchiseepayments.Count <= 0) ? (t.FeeValue.HasValue ? t.FeeValue.Value : 0) : (t.FeeValue.Value - t.Tblfranchiseepayments.Where(p => p.FeeDetailId == t.Id).Select(t => t.RecievedPayment.Value).Sum()),
                         PreviousPaidAmount = t.Tblfranchiseepayments.Where(p => p.FeeDetailId == t.Id).Select(t => t.RecievedPayment.Value).Sum(),
                         PreviousBalanceAmount = (t.Tblfranchiseepayments.Count <= 0) ? (t.FeeValue.HasValue ? t.FeeValue.Value : 0) : (t.FeeValue.Value - t.Tblfranchiseepayments.Where(p => p.FeeDetailId == t.Id).Select(t => t.RecievedPayment.Value).Sum()),
                         PaymentTerms = t.PaymentTerms,
                         PaymentDueDate = t.PaymentDueDate.HasValue ? t.PaymentDueDate.Value.ToString("yyyy-MM-dd") : string.Empty
                     }).ToListAsync();

                feeDetail.FeeDetails = result;
                feeDetail.PaymentDetails = query.Select(t => t.Tblfranchiseepayments.Select(k => new PaymentDetailQueryModel
                {
                    FeeType = k.FeeDetail.FeeType.Name,
                    PaymentRef = k.PaymentReference,
                    PaymentDate = k.PaymentDate.HasValue ? k.PaymentDate.Value.ToString("yyyy-MM-dd") : string.Empty,
                    PaymentType = k.PaymentType.PaymentType,
                    PaidAmout = k.TotalAmountPaid.HasValue ? k.TotalAmountPaid.Value : 0
                }).ToList()).FirstOrDefault();

                if (result != null)
                    return new ApiResult<FranchiseeFeeDetail>(new ApiResultCode(ApiResultType.Success), feeDetail);

                return new ApiResult<FranchiseeFeeDetail>(new ApiResultCode(ApiResultType.Error, messageText: "Error during Update"));
            }
            catch (System.Exception ex)
            {
                ErrorTrace.Logger(LogArea.RepositoryLayer, ex);
                return new ApiResult<FranchiseeFeeDetail>(new ApiResultCode(ApiResultType.Error, messageText: "Error during Update"));
            }
        }
    }
}
