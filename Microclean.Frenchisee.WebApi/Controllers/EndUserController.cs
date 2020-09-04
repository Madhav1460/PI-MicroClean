using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Microclean.Frenchisee.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EndUserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EndUserController(IMediator mediator)
        {
            _mediator = mediator;
        }
    }
}
