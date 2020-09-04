using Microsoft.AspNetCore.Mvc;
using System.Net;
using Utilities.ApiRisponse.IResponseUtil;

namespace Utilities.ApiRisponse
{
    public static class ResponseExtensions
    {
        public static IActionResult ToHttpResponse(this IResponse response)
        {
            var status = HttpStatusCode.OK;

            return new ObjectResult(response)
            {
                StatusCode = (int)status
            };
        }

        public static IActionResult ToHttpResponse<TModel>(this ISingleResponse<TModel> response)
        {
            var status = HttpStatusCode.OK;

            if (!response.Status)
                status = HttpStatusCode.OK;

            else if (!response.Status && response.Data == null)
                status = HttpStatusCode.NotFound;

            return new ObjectResult(response)
            {
                StatusCode = (int)status
            };
        }

        public static IActionResult ToHttpResponse<TModel>(this IListResponse<TModel> response)
        {
            var status = HttpStatusCode.OK;

            if (!response.Status)
                status = HttpStatusCode.OK;

            else if (!response.Status && response.Data == null)
                status = HttpStatusCode.NotFound;

            return new ObjectResult(response)
            {
                StatusCode = (int)status
            };
        }
    }
}
