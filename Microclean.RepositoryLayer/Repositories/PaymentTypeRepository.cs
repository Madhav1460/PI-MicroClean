using Microclean.CommandQueryLayer.QueryModels;
using Microclean.DataAccessLayer.Core;
using Microclean.DataModel;
using Microclean.DataModel.Models;
using Microclean.RepositoryLayer.IRepositories;
using MySqlX.XDevAPI.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Results;

namespace Microclean.RepositoryLayer.Repositories
{
    public class PaymentTypeRepository : IPaymentTypeRepository
    {
        private readonly IUnitOfWork<MicrocleanDbContext> _unit;

        public PaymentTypeRepository(IUnitOfWork<MicrocleanDbContext> unit)
        {
            _unit = unit;
        }

        public async Task<ApiResult<IEnumerable<PaymentTypeQueryModel>>> GetAllPaymentType()
        {
            var result = await _unit.GetRepository<Tblfeetype>().GetSelectedDataAsync
                (t => new PaymentTypeQueryModel { 
                    Id = t.Id,
                    Name = t.Name
                });
            if (result != null)
            {
                return new ApiResult<IEnumerable<PaymentTypeQueryModel>>(new ApiResultCode(ApiResultType.Success), result.UserObject);
            }
            return new ApiResult<IEnumerable<PaymentTypeQueryModel>>(new ApiResultCode(ApiResultType.Success,messageText: "No data found"));
        }
    }
}
