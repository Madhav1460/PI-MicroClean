using System;
using System.Collections.Generic;

namespace Microclean.DataModel.Models
{
    public partial class Tblstate
    {
        public Tblstate()
        {
            Tbladdress = new HashSet<Tbladdress>();
            Tblcity = new HashSet<Tblcity>();
            Tbldistrict = new HashSet<Tbldistrict>();
        }

        public int Id { get; set; }
        public int? CountryId { get; set; }
        public string Name { get; set; }
        public byte? Status { get; set; }
        public DateTime? InsertDate { get; set; }
        public long? InsertedBy { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public long? LastUpdatedBy { get; set; }

        public virtual Tblcountry Country { get; set; }
        public virtual ICollection<Tbladdress> Tbladdress { get; set; }
        public virtual ICollection<Tblcity> Tblcity { get; set; }
        public virtual ICollection<Tbldistrict> Tbldistrict { get; set; }
    }
}
