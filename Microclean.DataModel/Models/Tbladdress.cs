using System;
using System.Collections.Generic;

namespace Microclean.DataModel.Models
{
    public partial class Tbladdress
    {
        public Tbladdress()
        {
            Tbluseraddressmapping = new HashSet<Tbluseraddressmapping>();
        }

        public long Id { get; set; }
        public string FullAddress { get; set; }
        public string Address { get; set; }
        public int? ZipCode { get; set; }
        public string LandMark { get; set; }
        public int? CountryId { get; set; }
        public int? StateId { get; set; }
        public long? CityId { get; set; }
        public long? DistrictId { get; set; }
        public long? CityLocationId { get; set; }
        public decimal? Longitude { get; set; }
        public decimal? Latitude { get; set; }
        public byte? Status { get; set; }
        public DateTime? InsertDate { get; set; }
        public long? InsertedBy { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public long? LastUpdatedBy { get; set; }

        public virtual Tblcity City { get; set; }
        public virtual Tblcitylocation CityLocation { get; set; }
        public virtual Tblcountry Country { get; set; }
        public virtual Tbldistrict District { get; set; }
        public virtual Tblstate State { get; set; }
        public virtual ICollection<Tbluseraddressmapping> Tbluseraddressmapping { get; set; }
    }
}
