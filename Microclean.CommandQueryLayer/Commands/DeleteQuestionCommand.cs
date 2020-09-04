using MediatR;
using Microclean.CommandQueryLayer.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microclean.CommandQueryLayer.Commands
{
    public class DeleteQuestionCommand : BaseRequest, IRequest<IActionResult>
    {
        public long Id { get; set; }
        public bool Status { get; set; }
    }
}
