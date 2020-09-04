using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microclean.CommandQueryLayer.QueryModels
{
    public class TestListQueryModel
    {
        public long TestId { get; set; }
        public string Test { get; set; }
        public byte? IsTestDone { get; set; }
    }
}
