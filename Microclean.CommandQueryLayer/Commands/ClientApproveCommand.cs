using MediatR;
using Microclean.CommandQueryLayer.Models;
using Microsoft.AspNetCore.Mvc;

namespace Microclean.CommandQueryLayer.Commands
{
    public class ClientApproveCommand :BaseRequest, IRequest<IActionResult>
    {
        public long Id { get; set; }
        public bool IsApproved { get; set; }
    }
}
