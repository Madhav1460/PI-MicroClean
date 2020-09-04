using MediatR;
using Microclean.CommandQueryLayer.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microclean.CommandQueryLayer.Commands
{
   public class UserTestMappingCommand : BaseRequest, IRequest<IActionResult>
    {
        public UserTestMappingCommand()
        {
            SelectedUsers = new List<UserTestMappingRequest>();
        }

        public long? TestId { get; set; }
        public List<UserTestMappingRequest> SelectedUsers { get; set; }
    }
}
