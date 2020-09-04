using System;
using System.Collections.Generic;

namespace Microclean.DataModel.Models
{
    public partial class Tbltestuserright
    {
        public long Id { get; set; }
        public long? UserId { get; set; }
        public long? TestId { get; set; }
        public byte? IsApproved { get; set; }
        public byte? Status { get; set; }
        public DateTime? InsertDate { get; set; }
        public long? InsertedBy { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public long? LastUpdatedBy { get; set; }

        public virtual Tbltest Test { get; set; }
        public virtual Tbluser User { get; set; }
    }
}
