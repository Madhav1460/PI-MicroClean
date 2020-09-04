using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microclean.CommandQueryLayer.Commands;
using Microclean.CommandQueryLayer.Filter;
using Microclean.CommandQueryLayer.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Microclean.Frenchisee.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly IMediator _mediator;

        public QuestionController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("createquestion")]
        public async Task<IActionResult> CreateQyuestion([FromForm] CreateQuestionCommand command)
        {
            var result = await _mediator.Send(command);
            return result;
        }
        [HttpGet("getquestions")]
        public async Task<IActionResult> GetQuestions([FromQuery] QuestionFilter query)
        {
            var result = await _mediator.Send(new GetQuestionFilterQuery(query));
            return result;
        }
        [HttpGet("getquestionbyid")]
        public async Task<IActionResult> GetQuestionById([FromQuery] QuestionFilter query)
        {
            var result = await _mediator.Send(new GetQuestionOptionByIdQuery(query));
            return result;
        }
        [HttpPost("deletequestion")]
        public async Task<IActionResult> DeleteQuestion([FromForm] DeleteQuestionCommand command)
        {
            var result = await _mediator.Send(command);
            return result;
        }
    }
}
