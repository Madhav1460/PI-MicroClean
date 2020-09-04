using MediatR;
using Microclean.CommandQueryLayer.Filter;
using Microclean.CommandQueryLayer.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microclean.CommandQueryLayer.Queries
{
    public class GetTestResultByTestIdQuery :BaseRequest, IRequest<IActionResult>
    {
        public GetTestResultByTestIdQuery(QuestionFilter filter)
        {
            Filter = filter;
        }
        public QuestionFilter Filter { get; set; }
    }
}
