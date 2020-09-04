using MediatR;
using Microclean.CommandQueryLayer.Commands;
using Microclean.CommandQueryLayer.Models;
using Microclean.RepositoryLayer.IRepositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Utilities.ApiRisponse;
using Utilities.ApiRisponse.ResponseUtil;
using Utilities.Logger;
using Utilities.Results;

namespace Microclean.ProviderLayer.Handlers.Commands
{
    public class DeleteQuestionCommandHandler : BaseRequest, IRequestHandler<DeleteQuestionCommand, IActionResult>
    {
        private readonly IQuestionRepository _question;

        public DeleteQuestionCommandHandler(IQuestionRepository question)
        {
            _question = question;
        }

        public async Task<IActionResult> Handle(DeleteQuestionCommand request, CancellationToken cancellationToken)
        {
            var _response = new Response();
            try
            {
                var result = await _question.DeleteQuestionAsync(request);
                if (result.ResultType == ApiResultType.Success)
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
