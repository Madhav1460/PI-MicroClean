using MediatR;
using Microclean.CommandQueryLayer.Filter;
using Microclean.CommandQueryLayer.Models;
using Microsoft.AspNetCore.Mvc;

namespace Microclean.CommandQueryLayer.Queries
{
    public class GetTestByCategoryId : BaseRequest, IRequest<IActionResult>
    {
        public GetTestByCategoryId(QuestionFilter filter)
        {
            Filter = filter;
        }
        public QuestionFilter Filter { get; set; }
    }
}
