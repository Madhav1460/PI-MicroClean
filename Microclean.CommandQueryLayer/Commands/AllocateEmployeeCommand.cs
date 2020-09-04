using MediatR;
using Microclean.CommandQueryLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microclean.CommandQueryLayer.Commands
{
    public class AllocateEmployeeCommand :BaseRequest, IRequest<IActionResult>
    {
        public long UserId { get; set; }
        public long EmployeeId { get; set; }
    }
}
