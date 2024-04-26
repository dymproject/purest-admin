// MIT 许可证
// 版权 © 2023-present https://github.com/dymproject/purest-admin作者 以及贡献者
// 作者或版权持有人都不对任何索赔、损害或其他责任负责，无论这些追责来自合同、侵权或其它行为中，
// 还是产生于、源于或有关于本软件以及本软件的使用或其它处置。

using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;

using SqlSugar;

using Volo.Abp.Uow;
using System.Reflection;

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
