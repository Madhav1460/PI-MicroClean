using Microsoft.AspNetCore.Http;
using System;

namespace Microclean.CommandQueryLayer.Commands
{
    public class UserDetailCommand
    {
        public long Id { get; set; }
        public string AlternateNumber { get; set; }
        public string OwnerPANCardNo { get; set; }
        public string OwnersAadharCardNo { get; set; }
        public string AlternateEmail { get; set; }
        public DateTime? Dob { get; set; }
        public IFormFile Image { get; set; }
    }
}
