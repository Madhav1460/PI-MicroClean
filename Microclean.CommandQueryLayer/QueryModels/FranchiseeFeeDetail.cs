using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microclean.CommandQueryLayer.QueryModels
{
    public class FranchiseeFeeDetail
    {
        public FranchiseeFeeDetail()
        {
            PaymentDetails = new List<PaymentDetailQueryModel>();
            FeeDetails = new List<CommercialFeeQueryMode>();
        }
        public List<CommercialFeeQueryMode> FeeDetails { get; set; }
        public List<PaymentDetailQueryModel> PaymentDetails { get; set; }
    }
}
