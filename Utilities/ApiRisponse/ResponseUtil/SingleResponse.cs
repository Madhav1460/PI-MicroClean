using Utilities.ApiRisponse.IResponseUtil;

namespace Utilities.ApiRisponse.ResponseUtil
{
    public class SingleResponse<TModel> : ISingleResponse<TModel>
    {
        public string Message { get; set; }
        public bool Status { get; set; }
        public int ErrorTypeCode { get; set; }
        public TModel Data { get; set; }
    }
}
