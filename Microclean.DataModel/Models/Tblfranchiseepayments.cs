using System;
using System.Collections.Generic;

namespace Microclean.DataModel.Models
{
    public partial class Tblfranchiseepayments
    {
        public long Id { get; set; }
        public long? UserId { get; set; }
        public long? FeeDetailId { get; set; }
        public short? PaymentTypeId { get; set; }
        public decimal? RecievedPayment { get; set; }
        public decimal? OtherFeePayment { get; set; }
        public decimal? TotalAmountPaid { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string PaymentReference { get; set; }
        public string Remark { get; set; }
        public byte? Status { get; set; }
        public DateTime? InsertDate { get; set; }
        public long? InsertedBy { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public long? LastUpdatedBy { get; set; }

        public virtual Tblfeedetail FeeDetail { get; set; }
        public virtual Tblpaymenttype PaymentType { get; set; }
        public virtual Tbluser User { get; set; }
    }
}
