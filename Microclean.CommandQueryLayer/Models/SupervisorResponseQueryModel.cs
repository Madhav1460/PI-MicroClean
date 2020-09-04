using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microclean.CommandQueryLayer.Models
{
    public class SupervisorResponseQueryModel
    {
        public long Id { get; set; }
        public string FullName { get; set; }
        public string EmailId { get; set; }
        public string PhoneNo { get; set; }
        public bool IsApproved { get; set; }
        public string ApproveStatus { get; set; }
    }
}
