// MIT 许可证
// 版权 © 2023-present dym 和所有贡献者
// 作者或版权持有人都不对任何索赔、损害或其他责任负责，无论这些追责来自合同、侵权或其它行为中，
// 还是产生于、源于或有关于本软件以及本软件的使用或其它处置。

using System;
using System.Diagnostics;
using System.Threading.Tasks;

using Furion.EventBus;
using Furion.FriendlyException;
using Furion.JsonSerialization;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace PurestAdmin.Web.Core.Filter;

/// <summary>
/// 请求拦截
/// </summary>
public class RequestAsyncActionFilter : IAsyncActionFilter
{
    /// <summary>
    /// 事件发布者
    /// </summary>
    private readonly IEventPublisher _eventPublisher;
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="eventPublisher"></param>
    public RequestAsyncActionFilter(IEventPublisher eventPublisher)
    {
        _eventPublisher = eventPublisher;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        // 获取控制器/操作描述器
        var controllerActionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;

        // 获取请求方法
        var actionMethod = controllerActionDescriptor.MethodInfo;

        // 获取 HttpContext 和 HttpRequest 对象
        var httpContext = context.HttpContext;
        var httpRequest = httpContext.Request;

        // 获取服务端 IPv4 地址
        var localIPv4 = httpContext.GetLocalIpAddressToIPv4();

        // 获取客户端 IPv4 地址
        var remoteIPv4 = httpContext.GetRemoteIpAddressToIPv4();

        // 获取请求的 Url 地址
        var requestUrl = Uri.UnescapeDataString(httpRequest.GetRequestUrlAddress());

        // 获取来源 Url 地址
        var refererUrl = Uri.UnescapeDataString(httpRequest.GetRefererUrlAddress());

        // 服务器环境
        var environmentName = httpContext.RequestServices.GetRequiredService<IWebHostEnvironment>().EnvironmentName;

        // 客户端浏览器信息
        var userAgent = httpRequest.Headers["User-Agent"];

        // 获取方法参数
        var parameterValues = context.ActionArguments;

        var sw = Stopwatch.StartNew();
        var resultContext = await next();
        sw.Stop();

        // 获取异常对象情况
        var exception = resultContext.Exception;
        // 判断是否是验证异常
        var isValidationException = exception is AppFriendlyException friendlyException && friendlyException.ValidationException;

        var log = new
        {
            ControllerName = controllerActionDescriptor.ControllerTypeInfo.Name,
            RequestUrl = requestUrl,
            RefererUrl = refererUrl,
            EnvironmentName = environmentName,
            LocalIp = localIPv4,
            RemoteIp = remoteIPv4,
            RequestSuccessed = isValidationException || exception == null,
            Useragent = userAgent,
            Params = parameterValues.Count > 0 ? JSON.Serialize(parameterValues) : "",
            //Response = JSON.Serialize(resultContext.Result),
            RequestAction = httpRequest.Method,
            Elapsedmilliseconds = sw.ElapsedMilliseconds,
        };
        //await _eventPublisher.PublishAsync(new ChannelEventSource("Create:RequestLog", log));
    }
}