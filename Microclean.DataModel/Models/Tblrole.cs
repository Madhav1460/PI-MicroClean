using System;
using System.Collections.Generic;

namespace Microclean.DataModel.Models
{
    public partial class Tblrole
    {
        public Tblrole()
        {
            Tbluserrolemapping = new HashSet<Tbluserrolemapping>();
        }

        public int Id { get; set; }
        public int? ClientId { get; set; }
        public string Name { get; set; }
        public byte? Status { get; set; }
        public DateTime? InsertDate { get; set; }
        public long? InsertedBy { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public long? LastUpdatedBy { get; set; }

        public virtual Tblclient Client { get; set; }
        public virtual ICollection<Tbluserrolemapping> Tbluserrolemapping { get; set; }
    }
}
