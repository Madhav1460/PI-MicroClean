using MediatR;
using Microclean.CommandQueryLayer.Filter;
using Microclean.CommandQueryLayer.Models;
using Microsoft.AspNetCore.Mvc;

namespace Microclean.CommandQueryLayer.Queries
{
    public class GetTestQuestionOptionForEndUserByTestId:BaseRequest ,IRequest<IActionResult>
    {
        public GetTestQuestionOptionForEndUserByTestId(QuestionFilter filter)
        {
            Filter = filter;
        }
        public QuestionFilter Filter { get; set; }
    }
}
