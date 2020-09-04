using System;
using System.Collections.Generic;

namespace Microclean.DataModel.Models
{
    public partial class Tblquestionoptions
    {
        public Tblquestionoptions()
        {
            Tblusersubmitedanswer = new HashSet<Tblusersubmitedanswer>();
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public long? QuestionId { get; set; }
        public byte? IsMatched { get; set; }
        public byte? Status { get; set; }
        public DateTime? InsertDate { get; set; }
        public long? InsertedBy { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public long? LastUpdatedBy { get; set; }

        public virtual Tblquestion Question { get; set; }
        public virtual ICollection<Tblusersubmitedanswer> Tblusersubmitedanswer { get; set; }
    }
}
