using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microclean.CommandQueryLayer.Queries
{
   public  class GetAllCategoryQuery : IRequest<IActionResult>
    {
    }
}
