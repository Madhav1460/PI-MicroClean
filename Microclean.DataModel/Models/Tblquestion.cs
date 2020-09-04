using System;
using System.Collections.Generic;

namespace Microclean.DataModel.Models
{
    public partial class Tblquestion
    {
        public Tblquestion()
        {
            Tblquestionoptions = new HashSet<Tblquestionoptions>();
            Tblusersubmitedanswer = new HashSet<Tblusersubmitedanswer>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public int? CategoryId { get; set; }
        public short? QuestionDuration { get; set; }
        public short? NoOfOptions { get; set; }
        public byte? Status { get; set; }
        public DateTime? InsertDate { get; set; }
        public long? InsertedBy { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public long? LastUpdatedBy { get; set; }

        public virtual Tblcategory Category { get; set; }
        public virtual ICollection<Tblquestionoptions> Tblquestionoptions { get; set; }
        public virtual ICollection<Tblusersubmitedanswer> Tblusersubmitedanswer { get; set; }
    }
}
