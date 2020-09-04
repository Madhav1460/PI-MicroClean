using MediatR;
using Microclean.CommandQueryLayer.Commands;
using Microclean.CommandQueryLayer.Models;
using Microclean.RepositoryLayer.IRepositories;
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
    public class DeleteTraningDocumentByIdHandler : BaseRequest, IRequestHandler<DeleteTraningDocumentById, IActionResult>
    {
        private readonly IUserTranninRepository _usertraing;

        public DeleteTraningDocumentByIdHandler(IUserTranninRepository usertraing)
        {
            _usertraing = usertraing;
        }

        public async Task<IActionResult> Handle(DeleteTraningDocumentById request, CancellationToken cancellationToken)
        {
            var _response = new Response();
            try
            {
                var result = await _usertraing.DeleteTraningDocumenentById(request);
                if (result.ResultType == Utilities.Results.ApiResultType.Success)
                {
                    _response.Status = true;
                    _response.Message = result.MessageText;
                }
                else
                {
                    _response.Status = false;
                    _response.Message = result.MessageText;

                }
                return _response.ToHttpResponse();
            }
            catch (Exception ex)
            {
                ErrorTrace.Logger(LogArea.ProviderLayer, ex);
                _response.Status = false;
                _response.Message = "Exception";
                return _response.ToHttpResponse();
            }
        }
    }
}
