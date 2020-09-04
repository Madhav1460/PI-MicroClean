using MediatR;
using Microclean.CommandQueryLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Microclean.CommandQueryLayer.Commands
{
    public class LOIDocumentUploadCommand :BaseRequest, IRequest<IActionResult>
    {
        public int DocumentId { get; set; }
        public long FranchiseeId { get; set; }
        public string Remark { get; set; }
        public IFormFile LoiDocFile { get; set; }
    }
}
