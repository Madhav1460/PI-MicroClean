using System;
using System.Collections.Generic;

namespace Microclean.DataModel.Models
{
    public partial class Tblmailingcontent
    {
        public int Id { get; set; }
        public string MailSubject { get; set; }
        public string MialBody { get; set; }
        public int? MailFor { get; set; }
        public byte? Status { get; set; }
    }
}
