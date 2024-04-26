// MIT 许可证
// 版权 © 2023-present https://github.com/dymproject/purest-admin作者 以及贡献者
// 作者或版权持有人都不对任何索赔、损害或其他责任负责，无论这些追责来自合同、侵权或其它行为中，
// 还是产生于、源于或有关于本软件以及本软件的使用或其它处置。

using System.Diagnostics;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;

using PurestAdmin.Multiplex.EventBus;

using Volo.Abp.EventBus.Local;

namespace PurestAdmin.Api.Host.Options;

public class AdminRequestLogFilter : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        // 获取控制器/操作描述器
        var controllerActionDescriptor = (ControllerActionDescriptor)context.ActionDescriptor;
        ArgumentNullException.ThrowIfNull(controllerActionDescriptor);

        // 获取 HttpContext 和 HttpRequest 对象
        var httpContext = context.HttpContext;
        var httpRequest = httpContext.Request;
        var feature = httpContext.Features.Get<IHttpConnectionFeature>();

        // 获取客户端 IPv4 地址
        var remoteIPv4 = httpContext.Request.Headers["X-Real-IP"].FirstOrDefault() ?? feature?.LocalIpAddress?.MapToIPv4().ToString();
        var requestMethord = httpRequest.Method;

        // 服务器环境
        var environmentName = httpContext.RequestServices.GetRequiredService<IWebHostEnvironment>().EnvironmentName;

        // 获取方法参数
        var parameterValues = context.ActionArguments;

        var sw = Stopwatch.StartNew();
        var resultContext = await next();
        sw.Stop();

        var localEventBus = httpContext.RequestServices.GetRequiredService<ILocalEventBus>();
        RequestLogEventData data = new()
        {
            ActionName = controllerActionDescriptor.ActionName,
            ClientIp = remoteIPv4,
            ControllerName = controllerActionDescriptor.ControllerName,
            ElapsedTime = sw.ElapsedMilliseconds,
            EnvironmentName = environmentName,
            IsSuccess = resultContext.Exception == null,
            RequestMethod = requestMethord
        };
        if (httpContext.GetEndpoint()?.Metadata?.GetMetadata<AllowAnonymousAttribute>() != null)
        {
            return;
        }
        await localEventBus.PublishAsync(data);
    }
}
