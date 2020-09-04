using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microclean.CommandQueryLayer.Queries
{
    public class GetAllCityLocationByDistrictIdQuery : IRequest<IActionResult>
    {
        public GetAllCityLocationByDistrictIdQuery(long districtId)
        {
            DistrictId = districtId;
        }

        public long DistrictId { get; }
    }
}
