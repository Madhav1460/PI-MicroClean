using Utilities.ApiRisponse.IResponseUtil;

namespace Utilities.ApiRisponse.ResponseUtil
{
    public class Response : IResponse
    {
        public string Message { get; set; }
        public bool Status { get; set; }
        public int ErrorTypeCode { get; set; }
    }
}
