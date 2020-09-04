using System;
using System.Collections.Generic;

namespace Microclean.DataModel.Models
{
    public partial class Tblfeetype
    {
        public Tblfeetype()
        {
            Tblfeedetail = new HashSet<Tblfeedetail>();
        }

        public short Id { get; set; }
        public string Name { get; set; }
        public int? ClientId { get; set; }
        public byte? Status { get; set; }

        public virtual Tblclient Client { get; set; }
        public virtual ICollection<Tblfeedetail> Tblfeedetail { get; set; }
    }
}
