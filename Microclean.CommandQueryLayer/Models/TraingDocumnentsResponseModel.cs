using System;
using System.Collections.Generic;
using System.Text;

namespace Microclean.CommandQueryLayer.Models
{
    public class TraingDocumnentsResponseModel
    {
        public long Id { get; set; }
        public short? DocTypeId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string DocumnetPath { get; set; }
    }
}
