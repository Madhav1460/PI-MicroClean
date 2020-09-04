using MediatR;
using Microclean.CommandQueryLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microclean.CommandQueryLayer.Commands
{
    public class FranchiseeItSelfProfileUpdateCommand : BaseRequest, IRequest<IActionResult>
    {
        public FranchiseeItSelfProfileUpdateCommand()
        {
            UserDetail = new UserDetailCommand();
            UserDocumentCommands = new List<DocumentCommand>();
            Address = new AddressRequestModel();
            FranchiseeFeeCommands = new List<FranchiseeFeeCommandModel>();
        }
        public int CompanyId { get; set; }
        public long UserId { get; set; }
        public string CompayName { get; set; }
        public string CompanyPANCardNo { get; set; }
        public string CompanyGSTNo { get; set; }
        public string CompanyAddress { get; set; }
        public long CompanyAddressCity { get; set; }
        public string CompayEmail { get; set; }
        public string CompayPhone { get; set; }
        public int CompanyPincode { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public IFormFile LoiFile { get; set; }
        public AddressRequestModel Address { get; set; }
        public UserDetailCommand UserDetail { get; set; }
        public List<DocumentCommand> UserDocumentCommands { get; set; }
        public List<FranchiseeFeeCommandModel> FranchiseeFeeCommands { get; set; }
    }
}
