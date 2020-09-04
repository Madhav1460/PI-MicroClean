using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microclean.CommandQueryLayer.Models
{
    public class SubmitOptionCommandModel
    {
        public long SubmitedAnswerId { get; set; }
        public long OptionId { get; set; }
        public bool IsMatched { get; set; }
    }
}
