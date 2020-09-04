using MediatR;
using Microclean.CommandQueryLayer.Filter;
using Microclean.CommandQueryLayer.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microclean.CommandQueryLayer.Queries
{
   public class GetQuestionCountQuery : BaseRequest, IRequest<IActionResult>
    {
        public GetQuestionCountQuery(QuestionFilter filter)
        {
            Filter = filter;
        }
        public QuestionFilter Filter { get; set; }
    }
}
