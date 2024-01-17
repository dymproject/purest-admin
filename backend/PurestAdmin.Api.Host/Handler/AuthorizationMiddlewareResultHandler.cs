// MIT 许可证
// 版权 © 2023-present https://github.com/dymproject/purest-admin作者 以及贡献者
// 作者或版权持有人都不对任何索赔、损害或其他责任负责，无论这些追责来自合同、侵权或其它行为中，
// 还是产生于、源于或有关于本软件以及本软件的使用或其它处置。

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;

using PurestAdmin.Core.OopsExtension;
using PurestAdmin.Multiplex.MultiplexUser;

using Volo.Abp;

namespace PurestAdmin.Api.Host.Handler;
public class AuthorizationMiddlewareResultHandler : IAuthorizationMiddlewareResultHandler
{
    public async Task HandleAsync(RequestDelegate next, HttpContext context, AuthorizationPolicy policy, PolicyAuthorizationResult authorizeResult)
    {
        if (authorizeResult.Succeeded)
        {
            var endpoint = context.GetEndpoint() ?? throw Oops.Bah("未找到路由端点");
            var pattern = ((RouteEndpoint)endpoint).RoutePattern;
            var hostEnvironment = context.RequestServices.GetService<IHostEnvironment>() ?? throw new BusinessException();
            var currentUser = context.RequestServices.GetService<ICurrentUser>() ?? throw new BusinessException();
            if (!hostEnvironment.IsDevelopment())
            {
                var interfaces = await currentUser.GetInterfacesAsync();
                var ownInterface = interfaces.Any(x => x.Path == $"/{pattern.RawText}" && x.RequestMethod.Equals(context.Request.Method, StringComparison.CurrentCultureIgnoreCase));
                if (!ownInterface)
                {
                    await context.ForbidAsync();
                    return;
                }
            }
        }
        else if (authorizeResult.Challenged)
        {
            await context.ChallengeAsync();
            return;
        }
        else if (authorizeResult.Forbidden)
        {
            await context.ForbidAsync();
            return;
        }
        await next(context);
    }
}
