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
    public class CreateQuestionCommand : BaseRequest, IRequest<IActionResult>
    {
        public CreateQuestionCommand()
        {
            Options = new List<CreateOptionCommand>();
        }
        public long? Id { get; set; }
        public short DurationId { get; set; }
        public string Name { get; set; }
        public short NoOfOption { get; set; }
        public int CategoryId { get; set; }
        public List<CreateOptionCommand> Options { get; set; }
    }
}
