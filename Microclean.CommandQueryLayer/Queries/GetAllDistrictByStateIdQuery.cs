using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microclean.CommandQueryLayer.Queries
{
    public class GetAllDistrictByStateIdQuery : IRequest<IActionResult>
    {
        public GetAllDistrictByStateIdQuery(int stateId)
        {
            StateId = stateId;
        }
        public int StateId { get; }
    }
}
