using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using MediatR;
using Microclean.CommandQueryLayer.Commands;
using Microclean.CommandQueryLayer.Filter;
using Microclean.CommandQueryLayer.Models;
using Microclean.CommandQueryLayer.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Microclean.Frenchisee.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TestController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("createtest")]
        public async Task<IActionResult> CreateTest([FromForm] CreateTestCommand command)
        {
            var result = await _mediator.Send(command);
            return result;
        }
        [HttpGet("gettestbycategoryid")]
        public async Task<IActionResult> GetTestByCategoryId([FromQuery] QuestionFilter filter)
        {
            var result = await _mediator.Send(new GetTestByCategoryId(filter));
            return result;
        }
        [HttpGet("gettestbyid")]
        public async Task<IActionResult> GetTestById([FromQuery] long testid)
        {
            var result = await _mediator.Send(new GetTestByIdQuery(testid));
            return result;
        }
        [HttpGet("getquestioncountbycategoryid")]
        public async Task<IActionResult> GetQuestionCountbyCategoryId([FromQuery] QuestionFilter filter)
        {
            var result = await _mediator.Send(new GetQuestionCountQuery(filter));
            return result;
        }
        [HttpPost("deletetest")]
        public async Task<IActionResult> DeleteTest([FromForm] DeleteTestCommand command)
        {
            var result = await _mediator.Send(command);
            return result;
        }
        [HttpPost("usertestmapping")]
        public async Task<IActionResult> UserTestMapping([FromForm] int testId, [FromForm] string userArr)
        {
            if (!string.IsNullOrEmpty(userArr) && userArr != "[]")
            {
                UserTestMappingCommand command = new UserTestMappingCommand();
                var resultdata = JsonConvert.DeserializeObject<List<UserTestMappingRequest>>(userArr);
                command.TestId = testId;
                command.SelectedUsers = resultdata;
                var result = await _mediator.Send(command);
                return result;
            }
            return Ok("Updated");
        }
        [HttpGet("getalluser")]
        public async Task<IActionResult> GetAllUser([FromQuery]QuestionFilter filter)
        {
            var result = await _mediator.Send(new GetAllTestUserQuery(filter));
            return result;
        }
    }
}
