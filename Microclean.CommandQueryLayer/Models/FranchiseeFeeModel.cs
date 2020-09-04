using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microclean.CommandQueryLayer.Models
{
    public class FranchiseeFeeModel
    {
        public int FranchiseeFeesId { get; set; }
        public int OtherFeesId { get; set; }
        public int FixedMonthlyFeesId { get; set; }
        public int ThresholdAmountsId { get; set; }
        public short FranchiseeFeeId { get; set; }
        public short OtherFeeId { get; set; }
        public short FixedMonthlyFeeId { get; set; }
        public short ThresholdAmountId { get; set; }
        public decimal TotalAmmount { get; set; }

        public long UserId { get; set; }
        public double FranchiseeFee { get; set; }
        public double OtherFee { get; set; }
        public double FixedMonthlyFee { get; set; }
        public double ThresholdAmount { get; set; }
        public string PaymentTerms { get; set; }

        public double FranchiseeFeePaidAmout { get; set; }
        public double OtherFeePaidAmout { get; set; }
        public double FixedMonthlyFeePaidAmout { get; set; }
        public double ThresholdAmountPaidAmount { get; set; }
        public string PaymentDueDate { get; set; }
    }
}
