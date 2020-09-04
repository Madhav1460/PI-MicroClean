using System;
using System.Collections.Generic;

namespace Microclean.DataModel.Models
{
    public partial class Tbltest
    {
        public Tbltest()
        {
            Tbltestuserright = new HashSet<Tbltestuserright>();
            Tblusersubmitedanswer = new HashSet<Tblusersubmitedanswer>();
        }

        public long Id { get; set; }
        public int? ClientId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? NoOfQuestion { get; set; }
        public short? TestDuration { get; set; }
        public int? CategoryId { get; set; }
        public DateTime? InsertDate { get; set; }
        public long? InsertedBy { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public long? LastUpdatedBy { get; set; }
        public byte? Status { get; set; }

        public virtual Tblcategory Category { get; set; }
        public virtual ICollection<Tbltestuserright> Tbltestuserright { get; set; }
        public virtual ICollection<Tblusersubmitedanswer> Tblusersubmitedanswer { get; set; }
    }
}
