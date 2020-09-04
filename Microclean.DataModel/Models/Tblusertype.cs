using System;
using System.Collections.Generic;

namespace Microclean.DataModel.Models
{
    public partial class Tblusertype
    {
        public Tblusertype()
        {
            Tbluser = new HashSet<Tbluser>();
        }

        public short Id { get; set; }
        public int? ClientId { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public byte? Status { get; set; }
        public DateTime? InsertDate { get; set; }
        public long? InsertedBy { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public long? LastUpdatedBy { get; set; }

        public virtual ICollection<Tbluser> Tbluser { get; set; }
    }
}
