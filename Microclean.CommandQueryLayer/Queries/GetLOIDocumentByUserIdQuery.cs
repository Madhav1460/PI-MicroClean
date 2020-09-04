using MediatR;
using Microclean.CommandQueryLayer.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microclean.CommandQueryLayer.Queries
{
    public class GetLOIDocumentByUserIdQuery :BaseRequest, IRequest<IActionResult>
    {
        public long FranchiseeId { get; }
        public GetLOIDocumentByUserIdQuery(long franchiseeId)
        {
            FranchiseeId = franchiseeId;
        }
    }
}
