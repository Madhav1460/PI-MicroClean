using MediatR;
using Microclean.CommandQueryLayer.Commands;
using Microclean.CommandQueryLayer.Models;
using Microclean.RepositoryLayer.IRepositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using Utilities.ApiRisponse;
using Utilities.ApiRisponse.ResponseUtil;
using Utilities.Results;

namespace Microclean.ProviderLayer.Handlers.Commands
{
    public class CreateQuestionCommandHandler : BaseRequest, IRequestHandler<CreateQuestionCommand, IActionResult>
    {
        private readonly IQuestionRepository _question;

        public CreateQuestionCommandHandler(IQuestionRepository question)
        {
            _question = question;
        }

        public async Task<IActionResult> Handle(CreateQuestionCommand request, CancellationToken cancellationToken)
        {
            var _response = new Response();
            var result = await _question.CreateQuestionAsync(request);
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
    }
}
