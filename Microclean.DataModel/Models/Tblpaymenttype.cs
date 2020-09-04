using System;
using System.Collections.Generic;

namespace Microclean.DataModel.Models
{
    public partial class Tblpaymenttype
    {
        public Tblpaymenttype()
        {
            Tblfranchiseepayments = new HashSet<Tblfranchiseepayments>();
        }

        public short Id { get; set; }
        public string PaymentType { get; set; }
        public byte? Status { get; set; }
        public DateTime? InsertDate { get; set; }
        public long? InsertedBy { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public long? LastUpdatedBy { get; set; }
        public int? ClientId { get; set; }

        public virtual ICollection<Tblfranchiseepayments> Tblfranchiseepayments { get; set; }
    }
}
