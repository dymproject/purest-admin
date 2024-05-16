// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

using System.Reflection;

using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;

using SqlSugar;

using Volo.Abp.Uow;

namespace PurestAdmin.Api.Host.Options;

public class AdminUnitOfWorkFilter : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        // 获取控制器/操作描述器
        var controllerActionDescriptor = (ControllerActionDescriptor)context.ActionDescriptor;
        ArgumentNullException.ThrowIfNull(controllerActionDescriptor);
        var httpContext = context.HttpContext;
        var configuration = httpContext.RequestServices.GetRequiredService<IConfiguration>();
        var enableGlobalUnitOfWork = configuration.GetValue<bool>("EnableGlobalUnitOfWork");
        var hasUnitOfWorkAttribute = controllerActionDescriptor.MethodInfo.GetCustomAttribute<UnitOfWorkAttribute>();
        if (hasUnitOfWorkAttribute != null || enableGlobalUnitOfWork)
        {
            using var client = httpContext.RequestServices.GetRequiredService<ISqlSugarClient>();
            await client.Ado.BeginTranAsync();
            var resultContext = await next();
            if (resultContext.Exception == null)
            {
                await client.Ado.CommitTranAsync();
            }
            else
            {
                await client.Ado.RollbackTranAsync();
            }
            return;
        }
        await next();
    }
}
