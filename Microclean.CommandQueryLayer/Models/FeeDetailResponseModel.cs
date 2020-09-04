using Microclean.CommandQueryLayer.QueryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microclean.CommandQueryLayer.Models
{
    public class FeeDetailResponseModel
    {
        public FeeDetailResponseModel()
        {
            PaymentDetails = new List<PaymentDetailQueryModel>();
        }
        public long FeeId { get; set; }
        public string FeeType { get; set; }
        public decimal FeeValue { get; set; }
        public string PaymentTerms { get; set; }
        public string PaymentDueDate { get; set; }
        public List<PaymentDetailQueryModel> PaymentDetails { get; set; }
    }
}
