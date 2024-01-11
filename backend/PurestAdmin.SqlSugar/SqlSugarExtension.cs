// MIT 许可证
// 版权 © 2023-present https://github.com/dymproject/purest-admin作者 以及贡献者
// 作者或版权持有人都不对任何索赔、损害或其他责任负责，无论这些追责来自合同、侵权或其它行为中，
// 还是产生于、源于或有关于本软件以及本软件的使用或其它处置。

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Volo.Abp;

using Yitter.IdGenerator;

namespace PurestAdmin.SqlSugar;
public static class SqlSugarExtension
{
    public static IServiceCollection AddSqlSugarService(this IServiceCollection services)
    {
        var configuration = services.GetConfiguration();
        //List<Type> types = App.EffectiveTypes.Where(a => !a.IsAbstract && a.IsClass && a.GetCustomAttributes(typeof(SugarTable), true)?.FirstOrDefault() != null).ToList();
        SqlSugarScope sqlSugar = new(
            new ConnectionConfig()
            {
                DbType = DbType.MySql,
                ConnectionString = configuration.GetConnectionString("default"),
                IsAutoCloseConnection = true,
            },
            db =>
            {
                #region 查询过滤器

                #endregion

                #region 插入和更新过滤器
                db.Aop.DataExecuting = (oldValue, entityInfo) =>
                {
                    var context = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext;
                    var userId = context?.User?.FindFirst("userid")?.Value ?? "0";
                    //inset生效
                    if (entityInfo.OperationType == DataFilterType.InsertByObject)
                    {
                        if (entityInfo.PropertyName == nameof(BaseEntity.CreateBy))
                        {
                            entityInfo.SetValue(userId);
                        }
                        if (entityInfo.PropertyName == nameof(BaseEntity.CreateTime))
                        {
                            entityInfo.SetValue(DateTime.Now);
                        }
                    }
                    //update生效        
                    if (entityInfo.OperationType == DataFilterType.UpdateByObject)
                    {
                        if (entityInfo.PropertyName == nameof(BaseEntity.UpdateBy))
                        {
                            entityInfo.SetValue(userId);
                        }
                        if (entityInfo.PropertyName == nameof(BaseEntity.UpdateTime))
                        {
                            entityInfo.SetValue(DateTime.Now);
                        }
                    }
                };
                #endregion

                #region SQL执行前
                db.Aop.OnLogExecuting = (sql, pars) =>
                {
                    //Console.WriteLine(sql);//输出sql
                    if (pars != null)
                    {
                        //Console.WriteLine(JSON.Serialize(pars.Select(it => it.ParameterName + ":" + it.Value)));//输出参数
                    }
                };
                #endregion

                #region SQL执行完
                db.Aop.OnLogExecuted = (sql, pars) =>
                {
                    //执行完了可以输出SQL执行时间 (OnLogExecutedDelegate) 
                    //Console.Write("time:" + db.Ado.SqlExecutionTime.ToString());
                };
                #endregion

                #region SQL报错
                db.Aop.OnError = (exp) =>//SQL报错
                {
                    //exp.sql 这样可以拿到错误SQL，性能无影响拿到ORM带参数使用的SQL
                    //exp.Sql.LogError<SqlString>();
                    //5.0.8.2 获取无参数化 SQL  对性能有影响，特别大的SQL参数多的，调试使用
                    //Console.WriteLine(UtilMethods.GetSqlString(DbType.Oracle, exp.Sql, exp.Parametres as SugarParameter[]));
                };
                #endregion
            });

        services.AddHttpContextAccessor();
        services.AddSingleton<ISqlSugarClient>(sqlSugar);
        services.AddScoped(typeof(Repository<>));
        return services;
    }

    public static IServiceCollection ReplaceSqlSugarSnowflakeIdService(this IServiceCollection services)
    {
        //程序启动时执行一次就行
        StaticConfig.CustomSnowFlakeFunc = () =>
        {
            return YitIdHelper.NextId();
        };
        return services;
    }
}
