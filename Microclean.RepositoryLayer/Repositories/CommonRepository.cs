using Microclean.CommandQueryLayer.Queries;
using Microclean.CommandQueryLayer.QueryModels;
using Microclean.DataAccessLayer.Core;
using Microclean.DataModel;
using Microclean.DataModel.Models;
using Microclean.RepositoryLayer.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Results;

namespace Microclean.RepositoryLayer.Repositories
{
    public class CommonRepository : ICommonRepository
    {
        private readonly IUnitOfWork<MicrocleanDbContext> _unit;

        public CommonRepository(IUnitOfWork<MicrocleanDbContext> unit)
        {
            _unit = unit;
        }
        public async Task<ApiResult<IEnumerable<StateQueryModel>>> GetAllState(int countryId)
        {
            var result = await _unit.GetRepository<Tblstate>().GetSelectedDataAsync(p => p.Status == 1 && p.CountryId == countryId, t => new StateQueryModel
            {
                StateId = t.Id,
                StaeName = t.Name,
            });
            var dataresult = result.UserObject.OrderBy(t => t.StaeName);
            if (result.HasSuccess)
                return new ApiResult<IEnumerable<StateQueryModel>>(new ApiResultCode(ApiResultType.Success), dataresult);

            return new ApiResult<IEnumerable<StateQueryModel>>(new ApiResultCode(ApiResultType.Error, messageText: "Data not found"));
        }
        public async Task<ApiResult<IEnumerable<DistrictQueryMode>>> GetAllDistrictByStateId(int stateid)
        {
            var result = await _unit.GetRepository<Tbldistrict>().GetSelectedDataAsync(p => p.Status == 1 && p.StateId == stateid, t => new DistrictQueryMode
            {
                Id = t.Id,
                Name  = t.Name
            });
            var dataresult = result.UserObject.OrderBy(t => t.Name);
            if (result.HasSuccess)
                return new ApiResult<IEnumerable<DistrictQueryMode>>(new ApiResultCode(ApiResultType.Success), dataresult);

            return new ApiResult<IEnumerable<DistrictQueryMode>>(new ApiResultCode(ApiResultType.Error, messageText: "Data not found"));
        }

        public async Task<ApiResult<IEnumerable<CityLocationQueryMode>>> GetAllCityLocationByDistrictId(long districtid)
        {
            var result = await _unit.GetRepository<Tblcitylocation>().GetSelectedDataAsync(p => p.Status == 1 && p.DistrictId == districtid, t => new CityLocationQueryMode
            {
                Id = t.Id,
                CityLocation = t.CityLocation,
                ExtendedCityLocation = t.ExtendedCityLocation,
                Pincode = t.Pincode
            });
            var dataresult = result.UserObject.OrderBy(t => t.CityLocation);
            if (result.HasSuccess)
                return new ApiResult<IEnumerable<CityLocationQueryMode>>(new ApiResultCode(ApiResultType.Success), dataresult);

            return new ApiResult<IEnumerable<CityLocationQueryMode>>(new ApiResultCode(ApiResultType.Error, messageText: "Data not found"));
        }

        public async Task<ApiResult<IEnumerable<GetAllCategoryQueryModel>>> GetAllCategory()
        {
            var result = await _unit.GetRepository<Tblcategory>().GetSelectedDataAsync( t => new GetAllCategoryQueryModel
            {
                Id = t.Id,
                CategoryName = t.Name,
            });
            var dataresult = result.UserObject.OrderBy(t => t.CategoryName);
            if (result.HasSuccess)
                return new ApiResult<IEnumerable<GetAllCategoryQueryModel>>(new ApiResultCode(ApiResultType.Success), dataresult);

            return new ApiResult<IEnumerable<GetAllCategoryQueryModel>>(new ApiResultCode(ApiResultType.Error, messageText: "Data not found"));
        }
    }
}
