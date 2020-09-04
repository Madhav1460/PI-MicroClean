using System.Collections.Generic;
using Utilities.ApiRisponse.IResponseUtil;

namespace Utilities.ApiRisponse.ResponseUtil
{
    public class ListResponse<TModel> : IListResponse<TModel>
    {
        public string Message { get; set; }
        public bool Status { get; set; }
        public int ErrorTypeCode { get; set; }
        public IEnumerable<TModel> Data { get; set; }
    }
}
