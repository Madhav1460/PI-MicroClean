using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microclean.CommandQueryLayer.Models
{
    public class UserTestMappingRequest
    {
        public long? UserTestId { get; set; }
        public long UserId { get; set; }
        public bool IsSelected { get; set; }
    }
}
