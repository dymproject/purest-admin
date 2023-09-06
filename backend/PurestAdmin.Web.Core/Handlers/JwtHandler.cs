// PurestAdmin
// Author:dym
// MIT License
// https://gitee.com/dymproject/purest

using System.Linq;
using System.Threading.Tasks;

using Furion;
using Furion.Authorization;
using Furion.FriendlyException;
using Furion.UnifyResult;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using PurestAdmin.Core.Multiplex;

namespace PurestAdmin.Web.Core
{
    public class JwtHandler : AppAuthorizeHandler
    {
        public override async Task<bool> PipelineAsync(AuthorizationHandlerContext context, DefaultHttpContext httpContext)
        {

            return await CheckAuthorzieAsync(httpContext);
        }
        /// <summary>
        /// 检查权限
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        private static async Task<bool> CheckAuthorzieAsync(DefaultHttpContext httpContext)
        {
            if (httpContext.Request.Method.ToLower() == "delete")//&& App.HostEnvironment.IsStaging())
            {
                UnifyContext.Fill("演示环境，禁止删除！");
                return false;
            }
            var endPoint = httpContext.GetEndpoint();
            if (endPoint == null)
            {
                UnifyContext.Fill("未找到路由端点！");
                return false;
            }

            // 解析服务
            var userManager = httpContext.RequestServices.GetRequiredService<IUserManager>();
            var interfaces = await userManager.GetInterfacesAsync(userManager.UserId);

            var pattern = ((RouteEndpoint)endPoint).RoutePattern;
            return interfaces.Any(x => x.Path == $"/{pattern.RawText}" && x.RequestMethod.ToLower() == httpContext.Request.Method.ToLower());
        }
    }
}