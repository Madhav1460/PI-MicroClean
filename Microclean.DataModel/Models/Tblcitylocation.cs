using System;
using System.Collections.Generic;

namespace Microclean.DataModel.Models
{
    public partial class Tblcitylocation
    {
        public Tblcitylocation()
        {
            Tbladdress = new HashSet<Tbladdress>();
            Tblclient = new HashSet<Tblclient>();
        }

        public long Id { get; set; }
        public string CityLocation { get; set; }
        public string ExtendedCityLocation { get; set; }
        public int? Pincode { get; set; }
        public long? DistrictId { get; set; }
        public byte? Status { get; set; }

        public virtual Tbldistrict District { get; set; }
        public virtual ICollection<Tbladdress> Tbladdress { get; set; }
        public virtual ICollection<Tblclient> Tblclient { get; set; }
    }
}
