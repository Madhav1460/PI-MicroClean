using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Microclean.CommandQueryLayer.Commands
{
    public class TraningDocumentUploadCommand : IRequest<IActionResult>
    {
        public short DocType { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile DocFile { get; set; }
    }
}
