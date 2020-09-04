using System;
using System.Collections.Generic;

namespace Microclean.DataModel.Models
{
    public partial class Tbluserdetail
    {
        public long Id { get; set; }
        public long? UserId { get; set; }
        public string AlternateNumber { get; set; }
        public string AlternateEmail { get; set; }
        public string OwnersAadharCardNo { get; set; }
        public string OwnerPancardNo { get; set; }
        public DateTime? Dob { get; set; }
        public string Image { get; set; }
        public string FullName { get; set; }
        public byte? Status { get; set; }
        public DateTime? InsertDate { get; set; }
        public long? InsertedBy { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public long? LastUpdatedBy { get; set; }

        public virtual Tbluser User { get; set; }
    }
}
