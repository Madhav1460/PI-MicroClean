using System;
using System.Collections.Generic;
using System.Text;

namespace Microclean.CommandQueryLayer.Models
{
    public class FranchiseeFeeCommandModel
    {
        public long FeeId { get; set; }
        public short? FeeTypeId { get; set; }
        public decimal? FeeValue { get; set; }
        public decimal? PaidAmmout { get; set; }
        public decimal? TotalFee { get; set; }
        public string PaymentTerms { get; set; }
        public string PaymentDueDate { get; set; }
    }
}
