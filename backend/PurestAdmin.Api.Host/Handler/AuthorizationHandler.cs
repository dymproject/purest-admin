// MIT 许可证
// 版权 © 2023-present https://github.com/dymproject/purest-admin作者 以及贡献者
// 作者或版权持有人都不对任何索赔、损害或其他责任负责，无论这些追责来自合同、侵权或其它行为中，
// 还是产生于、源于或有关于本软件以及本软件的使用或其它处置。

using Microsoft.AspNetCore.Authorization;

using PurestAdmin.Multiplex.MultiplexUser;

using Volo.Abp;

namespace PurestAdmin.Api.Host.Handler;
public class AuthorizationHandler : IAuthorizationHandler
{
    private readonly IHostEnvironment _hostEnvironment;
    private readonly ICurrentUser _currentUser;
    public AuthorizationHandler(IHostEnvironment hostEnvironment, ICurrentUser currentUser)
    {
        _currentUser = currentUser;
        _hostEnvironment = hostEnvironment;
    }
    public async Task HandleAsync(AuthorizationHandlerContext context)
    {
        var httpContext = context.Resource as HttpContext;
        if (httpContext != null)
        {
            var endpoint = httpContext.GetEndpoint() as RouteEndpoint;
            var pattern = endpoint?.RoutePattern;
            if (!_hostEnvironment.IsDevelopment())
            {
                var interfaces = await _currentUser.GetInterfacesAsync();
                var ownInterface = interfaces.Any(x => x.Path == $"/{pattern?.RawText}" && x.RequestMethod.Equals(httpContext.Request.Method, StringComparison.CurrentCultureIgnoreCase));
                if (!ownInterface)
                {
                    context.Fail();
                    return;
                }
            }
        }



        //如果你有其他授权策略，请修改这里
        await Task.CompletedTask;
    }
}
