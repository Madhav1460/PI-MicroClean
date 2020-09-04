using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microclean.CommandQueryLayer.QueryModels
{
    public class UserDetailQueryModel 
    {
        public long? Id { get; set; }
        public string AlternateNumber { get; set; }
        public string AlternateEmail { get; set; }
        public DateTime? Dob { get; set; }
        public string ImagePath { get; set; }
        public string FullName { get; set; }
        public string UserAdharCardNo { get; set; }
        public string UserPAN { get; set; }
    }
}
