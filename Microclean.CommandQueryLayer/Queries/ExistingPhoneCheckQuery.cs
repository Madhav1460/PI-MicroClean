using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microclean.CommandQueryLayer.Queries
{
    public class ExistingPhoneCheckQuery : IRequest<IActionResult>
    {
        public ExistingPhoneCheckQuery(string phone)
        {
            Phone = phone;
        }
        public string Phone { get; }
    }
}
