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
    public class GetQuestionFilterQuery : BaseRequest,IRequest<IActionResult>
    {
        public GetQuestionFilterQuery(QuestionFilter filter)
        {
            Filter = filter;
        }
        public QuestionFilter Filter { get; set; }
    }
}
