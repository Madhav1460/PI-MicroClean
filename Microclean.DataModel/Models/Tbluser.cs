using System;
using System.Collections.Generic;

namespace Microclean.DataModel.Models
{
    public partial class Tbluser
    {
        public Tbluser()
        {
            Tblfeedetail = new HashSet<Tblfeedetail>();
            Tblfranchiseepayments = new HashSet<Tblfranchiseepayments>();
            Tbltestuserright = new HashSet<Tbltestuserright>();
            Tbluseraddressmapping = new HashSet<Tbluseraddressmapping>();
            Tbluserdetail = new HashSet<Tbluserdetail>();
            Tbluserdoument = new HashSet<Tbluserdoument>();
            Tbluserrolemapping = new HashSet<Tbluserrolemapping>();
            Tblusersubmitedanswer = new HashSet<Tblusersubmitedanswer>();
        }

        public long Id { get; set; }
        public int? ClientId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string NormalizedUserName { get; set; }
        public byte? EmailConfirmed { get; set; }
        public byte? PhoneNumberConfirmed { get; set; }
        public int? PassIncorrectNoOfTimes { get; set; }
        public int? AccessFailedCount { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordQuestion { get; set; }
        public string PasswordAnswer { get; set; }
        public DateTime? LastLogin { get; set; }
        public string DeviceType { get; set; }
        public string DeviceId { get; set; }
        public byte? IsGuestUser { get; set; }
        public string Fcmtoken { get; set; }
        public short? UserTypeId { get; set; }
        public DateTime? InsertDate { get; set; }
        public long? InsertedBy { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public long? LastUpdatedBy { get; set; }
        public byte IsApproved { get; set; }
        public long? ContactPersonId { get; set; }
        public byte? Status { get; set; }

        public virtual Tblclient Client { get; set; }
        public virtual Tblusertype UserType { get; set; }
        public virtual ICollection<Tblfeedetail> Tblfeedetail { get; set; }
        public virtual ICollection<Tblfranchiseepayments> Tblfranchiseepayments { get; set; }
        public virtual ICollection<Tbltestuserright> Tbltestuserright { get; set; }
        public virtual ICollection<Tbluseraddressmapping> Tbluseraddressmapping { get; set; }
        public virtual ICollection<Tbluserdetail> Tbluserdetail { get; set; }
        public virtual ICollection<Tbluserdoument> Tbluserdoument { get; set; }
        public virtual ICollection<Tbluserrolemapping> Tbluserrolemapping { get; set; }
        public virtual ICollection<Tblusersubmitedanswer> Tblusersubmitedanswer { get; set; }
    }
}
