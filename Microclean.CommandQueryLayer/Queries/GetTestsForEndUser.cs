using MediatR;
using Microclean.CommandQueryLayer.Models;
using Microsoft.AspNetCore.Mvc;

namespace Microclean.CommandQueryLayer.Queries
{
    public class GetTestsForEndUser :BaseRequest, IRequest<IActionResult>
    {
        
    }
}
