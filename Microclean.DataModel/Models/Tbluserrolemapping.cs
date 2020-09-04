using System;
using System.Collections.Generic;

namespace Microclean.DataModel.Models
{
    public partial class Tbluserrolemapping
    {
        public int Id { get; set; }
        public int? RoleId { get; set; }
        public long? UserId { get; set; }

        public virtual Tblrole Role { get; set; }
        public virtual Tbluser User { get; set; }
    }
}
