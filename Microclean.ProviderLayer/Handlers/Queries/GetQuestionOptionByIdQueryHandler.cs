using MediatR;
using Microclean.CommandQueryLayer.Commands;
using Microclean.CommandQueryLayer.Models;
using Microclean.CommandQueryLayer.Queries;
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

namespace Microclean.ProviderLayer.Handlers.Queries
{
    public class GetQuestionOptionByIdQueryHandler : BaseRequest, IRequestHandler<GetQuestionOptionByIdQuery, IActionResult>
    {
        private IQuestionRepository _question;

        public GetQuestionOptionByIdQueryHandler(IQuestionRepository question)
        {
            _question = question;
        }

        public async Task<IActionResult> Handle(GetQuestionOptionByIdQuery request, CancellationToken cancellationToken)
        {
            var _response = new SingleResponse<CreateQuestionCommand>();
            var result = await _question.GetQuestionOptionByIdAsync(request);
            if (result.HasSuccess)
            {
                _response.Data = result.UserObject;
                _response.Status = true;
            }
            else
            {
                _response.Message = result.ResultCode.MessageText;
                _response.Status = true;
            }
            return _response.ToHttpResponse();
        }
    }
}
