using System;
using System.Collections.Generic;

namespace Microclean.DataModel.Models
{
    public partial class Tblcity
    {
        public Tblcity()
        {
            Tbladdress = new HashSet<Tbladdress>();
        }

        public long Id { get; set; }
        public int? StateId { get; set; }
        public string Name { get; set; }
        public byte? Status { get; set; }
        public DateTime? InsertDate { get; set; }
        public long? InsertedBy { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public long? LastUpdatedBy { get; set; }

        public virtual Tblstate State { get; set; }
        public virtual ICollection<Tbladdress> Tbladdress { get; set; }
    }
}
