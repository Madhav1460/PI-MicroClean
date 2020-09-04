using MediatR;
using Microclean.CommandQueryLayer.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microclean.CommandQueryLayer.Commands
{
    public class SubmitTestCommand : IRequest<IActionResult>
    {
        public SubmitTestCommand()
        {
            QuestionsCommand = new List<SubmitQuestionCommandModel>();
        }
        public long UserId { get; set; }
        public long TestId { get; set; }
        public List<SubmitQuestionCommandModel> QuestionsCommand { get; set; }
    }
}
