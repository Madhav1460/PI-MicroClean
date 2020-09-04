using MediatR;
using Microclean.CommandQueryLayer.Commands;
using Microclean.CommandQueryLayer.Models;
using Microclean.DataModel.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Utilities.ApiRisponse;
using Utilities.ApiRisponse.ResponseUtil;
using Utilities.Logger;

namespace Microclean.ProviderLayer.Handlers.Commands
{
    public class UpdateFranchiseeByTheCompanyCommandHandler : BaseRequest, IRequestHandler<UpdateFranchiseeByTheCompanyCommand, IActionResult>
    {
        public Task<IActionResult> Handle(UpdateFranchiseeByTheCompanyCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
