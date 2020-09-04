using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microclean.CommandQueryLayer.Models
{
    public class BaseRequest
    {
        public long? CurrentUserId { get; set; }
        public short CurrentUserTypeId { get; set; }
        public string CurrentUserType { get; set; }
        public string CurrentUserName { get; set; }
        public string CurrentUserEmail { get; set; }
        public string CurrentUserPhone { get; set; }
        public int CurrentRoleId { get; set; }
        public string CurrentUserRole { get; set; }
        public int CurrentCientId { get; set; }
        public string CurrentClientName { get; set; }
    }
}
