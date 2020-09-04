using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microclean.CommandQueryLayer.Commands;
using Microclean.CommandQueryLayer.Filter;
using Microclean.CommandQueryLayer.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Microclean.Frenchisee.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EndUseTestController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EndUseTestController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("getendusertestbytestid")]
        public async Task<IActionResult> GetTraningDocument([FromQuery] QuestionFilter filter)
        {
            var result = await _mediator.Send(new GetTestQuestionOptionForEndUserByTestId(filter));
            return result;
        }

        [HttpGet("getendusertests")]
        public async Task<IActionResult> GetEndUserTests([FromQuery] QuestionFilter filter)
        {
            var result = await _mediator.Send(new GetTestsForEndUser());
            return result;
        }
        [HttpPost("submittest")]
        public async Task<IActionResult> SubmitTestAsync([FromForm] SubmitTestCommand command)
        {
            var result = await _mediator.Send(command);
            return result;
        }

        [HttpGet("gettestresultbytestid")]
        public async Task<IActionResult> GetTestResultByTestId([FromQuery] QuestionFilter filter)
        {
            var result = await _mediator.Send(new GetTestResultByTestIdQuery(filter));
            return result;
        }
    }
}
