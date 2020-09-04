using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microclean.CommandQueryLayer.Models
{
    public class UserClameResponse
    {
        public long? UserId { get; set; }
        public short? UserTypeId { get; set; }
        public string UserType { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int? RoleId { get; set; }
        public string Role { get; set; }
        public int? ClinetId { get; set; }
        public string ClinetName { get; set; }
        //public byte IsApproved { get; set; }
    }
}
