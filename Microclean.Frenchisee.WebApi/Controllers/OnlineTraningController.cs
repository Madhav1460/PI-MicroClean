using System.Threading.Tasks;
using MediatR;
using Microclean.CommandQueryLayer.Commands;
using Microclean.CommandQueryLayer.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Microclean.Frenchisee.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OnlineTraningController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OnlineTraningController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("uploadtrainingdoc")]
        [RequestFormLimits(MultipartBodyLengthLimit = 100097152)]
        [RequestSizeLimit(100097152)]
        public async Task<IActionResult> GetdoGumentbyFranchiseeId([FromForm] TraningDocumentUploadCommand command)
        {
            var result = await _mediator.Send(command);
            return result;
        }
        [HttpGet("gettraningdocument")]
        public async Task<IActionResult> GetTraningDocument()
        {
            var result = await _mediator.Send(new GetAllTraingDocumnentsQuery());
            return result;
        }
        [HttpPost("deletetraningdocumentbyid")]
        public async Task<IActionResult> DeletTraningDocumentById([FromForm] DeleteTraningDocumentById command)
        {
            var result = await _mediator.Send(command);
            return result;
        }
    }
}
