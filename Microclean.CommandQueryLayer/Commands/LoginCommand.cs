using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Microclean.CommandQueryLayer.Commands
{
    public class LoginCommand : IRequest<IActionResult>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
