using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microclean.CommandQueryLayer.Models
{
    public class KycDocumetResposneModel
    {
        public int DocId { get; set; }
        public short? DocumentType { get; set; }
        public string DocPath { get; set; }
        public string DocName { get; set; }
    }
}
