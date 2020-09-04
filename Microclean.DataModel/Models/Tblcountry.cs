using System;
using System.Collections.Generic;

namespace Microclean.DataModel.Models
{
    public partial class Tblcountry
    {
        public Tblcountry()
        {
            Tbladdress = new HashSet<Tbladdress>();
            Tblstate = new HashSet<Tblstate>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Sortname { get; set; }
        public byte? Status { get; set; }
        public int? Phonecode { get; set; }
        public DateTime? InsertDate { get; set; }
        public long? InsertedBy { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public long? LastUpdatedBy { get; set; }

        public virtual ICollection<Tbladdress> Tbladdress { get; set; }
        public virtual ICollection<Tblstate> Tblstate { get; set; }
    }
}
