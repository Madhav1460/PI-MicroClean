using Microclean.CommandQueryLayer.QueryModels;
using Microclean.DataModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Results;

namespace Microclean.RepositoryLayer.IRepositories
{
    public interface IPaymentRepository
    {
        Task<ApiResultCode> AddUpdateFee(List<Tblfeedetail> datamodel, short? paymenttypeid = null,string paymentref = null);
        Task<ApiResult<FranchiseeFeeDetail>> GetCommercialFeeByUserId(long userId);
    }
}
