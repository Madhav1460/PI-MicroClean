using Microclean.CommandQueryLayer.QueryModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Results;

namespace Microclean.RepositoryLayer.IRepositories
{
    public interface IPaymentTypeRepository
    {
        Task<ApiResult<IEnumerable<PaymentTypeQueryModel>>> GetAllPaymentType();
    }
}
