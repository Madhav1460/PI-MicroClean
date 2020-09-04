using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microclean.CommandQueryLayer.Queries
{
   public class ExistingEmailCheckQuery : IRequest<IActionResult>
    {
        public ExistingEmailCheckQuery(string email)
        {
            Email = email;
        }
        public string Email { get;}

    }
}
