using MediatR;
using Microclean.CommandQueryLayer.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microclean.CommandQueryLayer.Commands
{
    public class AddNewFranchiseeUserCommand : BaseRequest, IRequest<IActionResult>
    {
        public AddNewFranchiseeUserCommand()
        {
            Address = new AddressRequestModel();
        }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string AlternateEmail { get; set; }
        public string AlternatePhone { get; set; }
        public string PasswordHash { get; set; }
        public string ConfirmPassword { get; set; }
        public string CompanyAddress { get; set; }
        public long CityLocationId { get; set; }
        public int Pincode { get; set; }
        public string CompanyName { get; set; }
        public string CompanyEmail { get; set; }
        public string CompanyPhone { get; set; }
        public short UserTypeId { get; set; }
        public AddressRequestModel Address { get; set; }
        public FranchiseeFeeModel FranchiseeFee { get; set; }
    }
}
