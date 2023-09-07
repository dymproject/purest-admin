// MIT 许可证
// 版权 © 2023-present dym 和所有贡献者
// 作者或版权持有人都不对任何索赔、损害或其他责任负责，无论这些追责来自合同、侵权或其它行为中，
// 还是产生于、源于或有关于本软件以及本软件的使用或其它处置。

using System.Linq;
using System.Threading.Tasks;

using Furion.Authorization;
using Furion.UnifyResult;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

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