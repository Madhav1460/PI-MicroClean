using Microclean.CommandQueryLayer.Commands;
using Microclean.CommandQueryLayer.Queries;
using Microclean.CommandQueryLayer.QueryModels;
using Microclean.DataModel.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Utilities.Results;

namespace Microclean.RepositoryLayer.IRepositories
{
    public interface ICompanyRepository
    {
        Task<ApiResult<IEnumerable<GetAllAdminEmployeeQuery>>> GetAllAdminEmployee();
        Task<ApiResultCode> AllocateSuperviser(AllocateEmployeeCommand datamodel);
        Task<ApiResult<CompanyDetailQueryModel>> GetUserDetailByUserIdAsync(long id);
        Task<ApiResult<CompanyDetailQueryModel>> AddNewFranchiseeByCompany(Tblclient datamodel);
    }
}