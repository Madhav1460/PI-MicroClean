using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microclean.CommandQueryLayer.Commands;
using Microclean.CommandQueryLayer.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Microclean.Frenchisee.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FranchiseeController : ControllerBase
    {
        private readonly IMediator _mediator;
        public FranchiseeController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [AllowAnonymous]
        [HttpPost("franchiseeItSelfRegistration")]
        public async Task<IActionResult> FranchiseeItSelfRegistration([FromForm] FranchiseeItSelfRegistrationCommand command)
        {
            var result = await _mediator.Send(command);
            return result;
        }
        [HttpPost("franchiseeitselfprofileupdate")]
        public async Task<IActionResult> FranchiseeItSelfProfileUpdate([FromForm] FranchiseeItSelfProfileUpdateCommand command)
        {
            var result = await _mediator.Send(command);
            return result;
        }

        [HttpPost("createuserbyfranchisee")]
        public async Task<IActionResult> CreateUserByFranchisee([FromForm] CreateNewUserCommand command)
        {
            var result = await _mediator.Send(command);
            return result;
        }
        [HttpGet("getalluser")]
        public async Task<IActionResult> GetAllUser()
        {
            var result = await _mediator.Send(new GetAllUserQuery());
            return result;
        }
        [HttpGet("getuserdetailbyid")]
        public async Task<IActionResult> GetUserDetailById([FromQuery] long userId)
        {
            var query = new GetUserByIdQuery(userId);
            var result = await _mediator.Send(query);
            return result;
        }
        [HttpGet("franchiseefeeadd")]
        public async Task<IActionResult> FranchiseefeeAddUpdate([FromForm] AddFranchiseeFeeCommand command)
        {
            var result = await _mediator.Send(command);
            return result;
        }
        [HttpPost("updatefranchiseeuser")]
        public async Task<IActionResult> UpdateFranchiseeUser([FromForm] UpdateUserCommand command)
        {
            var result = await _mediator.Send(command);
            return result;
        }
        [HttpGet("existingtestnamecheck")]
        public async Task<IActionResult> ExistingTestNameCheck(string testname)
        {
            var result = await _mediator.Send(new ExistingTestNameCheckQuery(testname));
            return result;
        }
    }
}
