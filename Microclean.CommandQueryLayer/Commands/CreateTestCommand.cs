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
    public class CreateTestCommand : BaseRequest, IRequest<IActionResult>
    {
        public long Id { get; set; }
        public int? CatetoryId { get; set; }
        public string Name { get; set; }
        public int NoOfQuestion { get; set; }
        public string StatrDate { get; set; }
        public string EndDate { get; set; }
        public string Description { get; set; }
    }
}
