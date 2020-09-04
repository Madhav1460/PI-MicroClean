using System;
using System.Collections.Generic;

namespace Microclean.DataModel.Models
{
    public partial class Tbldistrict
    {
        public Tbldistrict()
        {
            Tbladdress = new HashSet<Tbladdress>();
            Tblcitylocation = new HashSet<Tblcitylocation>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public int? StateId { get; set; }
        public byte? Status { get; set; }
        public int? ClientId { get; set; }

        public virtual Tblstate State { get; set; }
        public virtual ICollection<Tbladdress> Tbladdress { get; set; }
        public virtual ICollection<Tblcitylocation> Tblcitylocation { get; set; }
    }
}
