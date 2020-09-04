using System;
using System.Collections.Generic;

namespace Microclean.DataModel.Models
{
    public partial class Tbltranningdocument
    {
        public long Id { get; set; }
        public short? DocType { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string DocumnetPath { get; set; }
        public byte? Status { get; set; }
    }
}
