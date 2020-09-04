using System;
using System.Collections.Generic;

namespace Microclean.DataModel.Models
{
    public partial class Tblfeedetail
    {
        public Tblfeedetail()
        {
            Tblfranchiseepayments = new HashSet<Tblfranchiseepayments>();
        }

        public long Id { get; set; }
        public int? ClientId { get; set; }
        public long? UserId { get; set; }
        public short? FeeTypeId { get; set; }
        public decimal? FeeValue { get; set; }
        public decimal? PaidAmmout { get; set; }
        public decimal? TotalFee { get; set; }
        public string PaymentTerms { get; set; }
        public DateTime? PaymentDueDate { get; set; }
        public DateTime? InsertDate { get; set; }
        public DateTime? Updatedate { get; set; }
        public long? InsertedBy { get; set; }
        public long? UpdatedBy { get; set; }

        public virtual Tblclient Client { get; set; }
        public virtual Tblfeetype FeeType { get; set; }
        public virtual Tbluser User { get; set; }
        public virtual ICollection<Tblfranchiseepayments> Tblfranchiseepayments { get; set; }
    }
}
