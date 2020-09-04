using MediatR;
using Microclean.CommandQueryLayer.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Microclean.Frenchisee.WebApi.Infrastructure
{
    public class UserIdPipe<TIn, TOut> : IPipelineBehavior<TIn, TOut>
    {
        private HttpContext httpContext;
        public UserIdPipe(IHttpContextAccessor accessor)
        {
            httpContext = accessor.HttpContext;
        }
        public async Task<TOut> Handle(TIn request, CancellationToken cancellationToken, RequestHandlerDelegate<TOut> next)
        {
            if (httpContext.User.Claims.Count() > 0)
            {
                if (request is BaseRequest br)
                {
                    br.CurrentUserId = Convert.ToInt64(httpContext.User.Claims.FirstOrDefault(x => x.Type.Equals("UserId")).Value);
                    br.CurrentUserTypeId = !string.IsNullOrEmpty(httpContext.User.Claims.FirstOrDefault(x => x.Type.Equals("UserTypeId")).Value) ? Convert.ToInt16(httpContext.User.Claims.FirstOrDefault(x => x.Type.Equals("UserTypeId")).Value) : Convert.ToInt16(0);
                    br.CurrentUserType = !string.IsNullOrEmpty(httpContext.User.Claims.FirstOrDefault(x => x.Type.Equals("UserType")).Value) ? httpContext.User.Claims.FirstOrDefault(x => x.Type.Equals("UserType")).Value : string.Empty;
                    br.CurrentUserName = httpContext.User.Claims.FirstOrDefault(x => x.Type.Equals("UserName")).Value ?? string.Empty;
                    br.CurrentUserEmail = httpContext.User.Claims.FirstOrDefault(x => x.Type.Equals("Email")).Value ?? string.Empty;
                    br.CurrentUserPhone = httpContext.User.Claims.FirstOrDefault(x => x.Type.Equals("Phone")).Value ?? string.Empty;
                    br.CurrentRoleId = !string.IsNullOrEmpty(httpContext.User.Claims.FirstOrDefault(x => x.Type.Equals("RoleId")).Value) ? Convert.ToInt32(httpContext.User.Claims.FirstOrDefault(x => x.Type.Equals("RoleId")).Value) : 0;
                    br.CurrentUserRole = !string.IsNullOrEmpty(httpContext.User.Claims.FirstOrDefault(x => x.Type.Equals("Role")).Value) ? httpContext.User.Claims.FirstOrDefault(x => x.Type.Equals("Role")).Value : string.Empty;
                    br.CurrentClientName = httpContext.User.Claims.FirstOrDefault(x => x.Type.Equals("ClinetName")).Value ?? string.Empty;
                    br.CurrentCientId = !string.IsNullOrEmpty(httpContext.User.Claims.FirstOrDefault(x => x.Type.Equals("ClinetId")).Value) ? Convert.ToInt32(httpContext.User.Claims.FirstOrDefault(x => x.Type.Equals("ClinetId")).Value) : Convert.ToInt16(0);
                }
            }
            var result = await next();
            return result;
        }
    }
}
