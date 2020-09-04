using System;
using System.Collections.Generic;

namespace Microclean.DataModel.Models
{
    public partial class Tbladdresstype
    {
        public Tbladdresstype()
        {
            Tbluseraddressmapping = new HashSet<Tbluseraddressmapping>();
        }

        public short Id { get; set; }
        public string Name { get; set; }
        public byte? Status { get; set; }
        public DateTime? InsertDate { get; set; }
        public long? InsertedBy { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public long? LastUpdatedBy { get; set; }

        public virtual ICollection<Tbluseraddressmapping> Tbluseraddressmapping { get; set; }
    }
}
