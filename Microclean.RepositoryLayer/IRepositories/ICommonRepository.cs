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
    public interface ICommonRepository
    {
        Task<ApiResult<IEnumerable<StateQueryModel>>> GetAllState(int countryId);
        Task<ApiResult<IEnumerable<DistrictQueryMode>>> GetAllDistrictByStateId(int stateid);
        Task<ApiResult<IEnumerable<CityLocationQueryMode>>> GetAllCityLocationByDistrictId(long districtid);
        Task<ApiResult<IEnumerable<GetAllCategoryQueryModel>>> GetAllCategory();
    }
}
