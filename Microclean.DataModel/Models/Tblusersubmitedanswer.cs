using System;
using System.Collections.Generic;

namespace Microclean.DataModel.Models
{
    public partial class Tblusersubmitedanswer
    {
        public long Id { get; set; }
        public long? QuestionId { get; set; }
        public long? OptionId { get; set; }
        public byte? IsMatched { get; set; }
        public long? UserId { get; set; }
        public long? TestId { get; set; }
        public byte? Status { get; set; }
        public DateTime? InsertDate { get; set; }
        public DateTime? LastUpdateDate { get; set; }

        public virtual Tblquestionoptions Option { get; set; }
        public virtual Tblquestion Question { get; set; }
        public virtual Tbltest Test { get; set; }
        public virtual Tbluser User { get; set; }
    }
}
