using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microclean.CommandQueryLayer.QueryModels
{
    public class PaymentDetailQueryModel
    {
        public string FeeType { get; set; }
        public string PaymentDate { get; set; }
        public string PaymentRef { get; set; }
        public string PaymentType { get; set; }
        public decimal PaidAmout { get; set; }
    }
}
