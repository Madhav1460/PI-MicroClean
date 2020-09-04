using MediatR;
using Microclean.CommandQueryLayer.Models;
using Microclean.CommandQueryLayer.Queries;
using Microclean.CommandQueryLayer.QueryModels;
using Microclean.RepositoryLayer.IRepositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using Utilities.ApiRisponse;
using Utilities.ApiRisponse.ResponseUtil;

namespace Microclean.ProviderLayer.Handlers.Queries
{
    public class GetQuestionFilterQueryHandler : BaseRequest, IRequestHandler<GetQuestionFilterQuery, IActionResult>
    {
        private IQuestionRepository _question;

        public GetQuestionFilterQueryHandler(IQuestionRepository question)
        {
            _question = question;
        }

        public async Task<IActionResult> Handle(GetQuestionFilterQuery request, CancellationToken cancellationToken)
        {
            var _response = new ListResponse<QuestionWithListQuery>();
            var result = await _question.GetQuestionDetailAsync(request);
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
