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
    public class GetCommercialFeeQueryByFranchiseeId : BaseRequest, IRequest<IActionResult>
    {
        public GetCommercialFeeQueryByFranchiseeId(long userId)
        {
            UserId = userId;
        }

        public long UserId { get; }
    }
}
