using System;
using System.Collections.Generic;

namespace Microclean.DataModel.Models
{
    public partial class Tblclient
    {
        public Tblclient()
        {
            Tbldocumenttype = new HashSet<Tbldocumenttype>();
            Tblfeedetail = new HashSet<Tblfeedetail>();
            Tblfeetype = new HashSet<Tblfeetype>();
            Tblrole = new HashSet<Tblrole>();
            Tbluser = new HashSet<Tbluser>();
            Tbluserdoument = new HashSet<Tbluserdoument>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string LogoPath { get; set; }
        public long? CityLocationid { get; set; }
        public string FullAddress { get; set; }
        public int? ZipCode { get; set; }
        public string PhoneNo { get; set; }
        public string Email { get; set; }
        public string CompanyPanCardNo { get; set; }
        public string CompanyGstNo { get; set; }
        public byte? Status { get; set; }
        public DateTime? InsertedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public virtual Tblcitylocation CityLocation { get; set; }
        public virtual ICollection<Tbldocumenttype> Tbldocumenttype { get; set; }
        public virtual ICollection<Tblfeedetail> Tblfeedetail { get; set; }
        public virtual ICollection<Tblfeetype> Tblfeetype { get; set; }
        public virtual ICollection<Tblrole> Tblrole { get; set; }
        public virtual ICollection<Tbluser> Tbluser { get; set; }
        public virtual ICollection<Tbluserdoument> Tbluserdoument { get; set; }
    }
}
