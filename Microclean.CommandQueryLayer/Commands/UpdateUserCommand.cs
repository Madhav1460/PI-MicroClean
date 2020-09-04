using MediatR;
using Microclean.CommandQueryLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microclean.CommandQueryLayer.Commands
{
    public class UpdateUserCommand : BaseRequest, IRequest<IActionResult>
    {
        public UpdateUserCommand()
        {
            UserDetail = new UserDetailCommand();
            UserDocumentCommands = new List<DocumentCommand>();
            Address = new AddressRequestModel();
        }
        public long UserId { get; set; }    
        public string CompayName { get; set; }
        public string CompayEmail { get; set; }
        public string CompayPhone { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public IFormFile LoiFile { get; set; }
        public AddressRequestModel Address { get; set; }
        public UserDetailCommand UserDetail { get; set; }
        public List<DocumentCommand> UserDocumentCommands { get; set; }
    }
}
