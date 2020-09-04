using System;
using System.Collections.Generic;

namespace Microclean.DataModel.Models
{
    public partial class Tblcategory
    {
        public Tblcategory()
        {
            Tblquestion = new HashSet<Tblquestion>();
            Tbltest = new HashSet<Tbltest>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Tblquestion> Tblquestion { get; set; }
        public virtual ICollection<Tbltest> Tbltest { get; set; }
    }
}
