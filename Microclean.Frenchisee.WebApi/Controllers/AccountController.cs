using System.Threading.Tasks;
using MediatR;
using Microclean.CommandQueryLayer.Commands;
using Microclean.CommandQueryLayer.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Microclean.Frenchisee.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("changepassword")]
        public async Task<IActionResult> ChangePassword([FromForm] ChangePasswordCommand command)
        {
            var result = await _mediator.Send(command);
            return result;
        }
        [AllowAnonymous]
        [HttpPost("weblogin")]
        public async Task<IActionResult> WebLogin([FromForm] LoginCommand command)
        {
            var result = await _mediator.Send(command);
            return result;
        }

    }
}