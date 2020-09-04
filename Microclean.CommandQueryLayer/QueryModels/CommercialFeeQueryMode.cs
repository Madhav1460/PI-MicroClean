using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microclean.CommandQueryLayer.QueryModels
{
    public class CommercialFeeQueryMode
    {
        public long FeeId { get; set; }
        public decimal FeeValue { get; set; }
        public short FeeTypeId { get; set; }
        public decimal PreviousPaidAmount { get; set; }
        public decimal PreviousBalanceAmount { get; set; }
        public decimal PaymentDue { get; set; }
        public decimal TotalAmount { get; set; }
        public string PaymentTerms { get; set; }
        public string PaymentDueDate { get; set; }
        public decimal PaidTotalAmmount { get; set; }
    }
}
