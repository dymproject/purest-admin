// MIT 许可证
// 版权 © 2023-present https://github.com/dymproject/purest-admin作者 以及贡献者
// 作者或版权持有人都不对任何索赔、损害或其他责任负责，无论这些追责来自合同、侵权或其它行为中，
// 还是产生于、源于或有关于本软件以及本软件的使用或其它处置。

using Microsoft.AspNetCore.Mvc.Filters;

using Volo.Abp.AspNetCore.Mvc;

namespace PurestAdmin.SqlSugar;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class UnitOfWorkAttribute : Attribute, IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var client = context.GetRequiredService<ISqlSugarClient>();
        // 获取动作方法描述器
        //var actionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;
        try
        {
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
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            await client.Ado.RollbackTranAsync();
        }
        finally
        {
            client.Dispose();
        }
    }
}
