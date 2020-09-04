using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microclean.CommandQueryLayer.Models;

namespace Microclean.CommandQueryLayer.Commands
{
    public class FranchiseeItSelfRegistrationCommand : IRequest<IActionResult>
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string CompanyName { get; set; }
        public string PasswordHash { get; set; }
        public string ConfirmPassword { get; set; }
        public long CityId { get; set; }
        public int Pincode { get; set; }
        public short UserTypeId { get; set; }
        public AddressRequestModel Address { get; set; }
    }
}
