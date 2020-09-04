using System;
using System.Collections.Generic;

namespace Microclean.DataModel.Models
{
    public partial class Tbluseraddressmapping
    {
        public long Id { get; set; }
        public long? UserId { get; set; }
        public long? AddressId { get; set; }
        public short? AddressTypeId { get; set; }
        public byte? IsDefaultDeliveryLocation { get; set; }
        public byte? Status { get; set; }
        public DateTime? InsertDate { get; set; }
        public long? InsertedBy { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public long? LastUpdatedBy { get; set; }

        public virtual Tbladdress Address { get; set; }
        public virtual Tbladdresstype AddressType { get; set; }
        public virtual Tbluser User { get; set; }
    }
}
