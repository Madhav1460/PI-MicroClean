using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.ApiRisponse.IResponseUtil
{
    public interface ISingleResponse<TModel> : IResponse
    {
        TModel Data { get; set; }
    }
}
