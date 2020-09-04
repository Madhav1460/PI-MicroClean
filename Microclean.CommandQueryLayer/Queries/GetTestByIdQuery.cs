using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microclean.CommandQueryLayer.Queries
{
    public class GetTestByIdQuery : IRequest<IActionResult>
    {
        public GetTestByIdQuery(long testid)
        {
            TestId = testid;
        }
        public long TestId { get; }
    }
}
