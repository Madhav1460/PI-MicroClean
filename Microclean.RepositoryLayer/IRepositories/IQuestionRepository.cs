using Microclean.CommandQueryLayer.Commands;
using Microclean.CommandQueryLayer.Models;
using Microclean.CommandQueryLayer.Queries;
using Microclean.CommandQueryLayer.QueryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Results;

namespace Microclean.RepositoryLayer.IRepositories
{
    public interface IQuestionRepository
    {
        Task<ApiResultCode> CreateQuestionAsync(CreateQuestionCommand request);
        Task<ApiResult<IList<QuestionWithListQuery>>> GetQuestionDetailAsync(GetQuestionFilterQuery filter);
        Task<ApiResult<CreateQuestionCommand>> GetQuestionOptionByIdAsync(GetQuestionOptionByIdQuery filter);
        Task<ApiResultCode> DeleteQuestionAsync(DeleteQuestionCommand datamodel);
    }
}
