using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microclean.CommandQueryLayer.Filter
{
    public class QuestionFilter
    {
        public int? CategoryId { get; set; }
        public int? TestId { get; set; }
        public long? QuestionId { get; set; }
    }
}
