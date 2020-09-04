using System;
using System.Collections.Generic;
using System.Text;

namespace Microclean.CommandQueryLayer.Models
{
    public class UserTestMappingRequestModel
    {
        public int TestId { get; set; }
        public List<UserTestMappingResponseModel> SelectedUsers { get; set; }
    }
}
