using MediatR;
using Microclean.CommandQueryLayer.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microclean.CommandQueryLayer.Commands
{
    public class CreateNewUserCommand : BaseRequest, IRequest<IActionResult>
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string PasswordHash { get; set; }
        public string ConfirmPassword { get; set; }
        public short  UserType { get; set; }
        public string AlternateEmail { get; set; }
        public string AlternatePhone { get; set; }
        public AddressRequestModel Address { get; set; }
    }
}
