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
    public class CompanyController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CompanyController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("getuserdetailbyid")]
        public async Task<IActionResult> GetUserDetailById([FromQuery] long userId)
        {
            var query = new GetUserByIdQuery(userId);
            var result = await _mediator.Send(query);
            return result;
        }
        [HttpGet("getallaocument")]
        public async Task<IActionResult> GetDocumentAllType()
        {
            var result = await _mediator.Send(new GetAllDocumentTypeQuery());
            return result;
        }
        [HttpGet("getallfranchisee")]
        public async Task<IActionResult> GetAllFranchisee()
        {
            var result = await _mediator.Send(new GetAllFranchiseeQuery());
            return result;
        }

        [HttpPost("franchiseeApprove")]
        public async Task<IActionResult> FranchiseeApprove([FromForm] ClientApproveCommand command)
        {
            var result = await _mediator.Send(command);
            return result;
        }

        [HttpPost("uploadloidocumentforfranchisee")]
        public async Task<IActionResult> GetdoGumentbyFranchiseeId([FromForm] LOIDocumentUploadCommand command)
        {
            var result = await _mediator.Send(command);
            return result;
        }
        [HttpGet("getdocumentbyfranchiseeid")]
        public async Task<IActionResult> GetdoGumentbyFranchiseeId([FromQuery] long franchiseeid)
        {
            var query = new GetLOIDocumentByUserIdQuery(franchiseeid);
            var result = await _mediator.Send(query);
            return result;
        }
        [AllowAnonymous]
        [HttpGet("getkycdocument")]
        public async Task<IActionResult> GetKycDocument(long franchiseeId)
        {
            var result = await _mediator.Send(new GetKycDocumentQueryByyfranchiseeId(franchiseeId));
            return result;
        }
        //[AllowAnonymous]
        [HttpPost("createfranchisee")]
        public async Task<IActionResult> CreateFranchisee([FromForm] AddNewFranchiseeUserCommand command)
        {
            var result = await _mediator.Send(command);
            return result;
        }
        //[AllowAnonymous]
        [HttpGet("getfeedetailbyuserid")]
        public async Task<IActionResult> GetFeeDetailByUserId([FromQuery] long userid)
        {
            var result = await _mediator.Send(new GetCommercialFeeQueryByFranchiseeId(userid));
            return result;
        }

        [HttpPost("addfranchiseefee")]
        public async Task<IActionResult> AddFranchiseeFee([FromForm] AddFranchiseeFeeCommand command)
        {
            var result = await _mediator.Send(command);
            return result;
        }

        [HttpPost("allocateemployee")]
        public async Task<IActionResult> AllocateEmployee([FromForm] AllocateEmployeeCommand command)
        {
            var result = await _mediator.Send(command);
            return result;
        }

        [HttpGet("getalladminemployee")]
        public async Task<IActionResult> GetAllAdminEmployee()
        {
            var result = await _mediator.Send(new GetAllAdminEmployeeQuery());
            return result;

        }
        [HttpPost("createsupervisorbyadmin")]
        public async Task<IActionResult> CreateCompanyUser([FromForm] CreateNewCompanyUserCommand command)
        {
            var result = await _mediator.Send(command);
            return result;
        }
        [HttpGet("getalladminsupervisor")]
        public async Task<IActionResult> GetAllCompanyUser()
        {
            var result = await _mediator.Send(new GetAllComapanyUserQuery());
            return result;
        }
        [HttpPost("updateadminemployee")]
        public async Task<IActionResult> UpdateAdminEmployee([FromForm] UpdateUserCommand command)
        {
            var result = await _mediator.Send(command);
            return result;
        }
    }
}
