using Microclean.CommandQueryLayer.QueryModels;
using Microclean.DataModel.Models;
using System.Threading.Tasks;
using Utilities.Results;

namespace Microclean.RepositoryLayer.IRepositories
{
    public interface IFranchiseeRepository
    {
        Task<ApiResultCode> FranchiseeUpdateItSelfAsync(Tblclient datamodel);
        Task<ApiResult<CompanyDetailQueryModel>> GetUserDetailByUserIdAsync(long id);
        Task<ApiResult<bool>> IsTestNameExist(string testname);
    }
}
