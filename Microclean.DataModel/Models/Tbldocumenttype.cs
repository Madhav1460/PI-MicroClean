using System;
using System.Collections.Generic;

namespace Microclean.DataModel.Models
{
    public partial class Tbldocumenttype
    {
        public Tbldocumenttype()
        {
            Tbluserdoument = new HashSet<Tbluserdoument>();
        }

        public short Id { get; set; }
        public int? ClientId { get; set; }
        public string Name { get; set; }
        public byte? Status { get; set; }
        public DateTime? InsertDate { get; set; }
        public long? InsertedBy { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public long? LastUpdatedBy { get; set; }

        public virtual Tblclient Client { get; set; }
        public virtual ICollection<Tbluserdoument> Tbluserdoument { get; set; }
    }
}
