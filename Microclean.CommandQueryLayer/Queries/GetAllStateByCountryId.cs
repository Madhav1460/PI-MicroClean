using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microclean.CommandQueryLayer.Queries
{
    public class GetAllStateByCountryId : IRequest<IActionResult>
    {
        public GetAllStateByCountryId(int countryId)
        {
            CountryId = countryId;
        }
        public int CountryId { get; }
    }
}
