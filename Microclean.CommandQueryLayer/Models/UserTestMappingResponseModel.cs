using System;
using System.Collections.Generic;
using System.Text;

namespace Microclean.CommandQueryLayer.Models
{
   public class UserTestMappingResponseModel
    {
        public string FullName { get; set; }
        public long? TestId { get; set; }
        public long UserId { get; set; }
        public bool Status { get; set; }
    }
}
