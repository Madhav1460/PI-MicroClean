using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Microclean.CommandQueryLayer.Queries
{
    public class GetKycDocumentQueryByyfranchiseeId : IRequest<IActionResult>
    {
        public long FranchiseeId { get; }
        public GetKycDocumentQueryByyfranchiseeId(long franchiseeId)
        {
            FranchiseeId = franchiseeId;
        }
    }
}
