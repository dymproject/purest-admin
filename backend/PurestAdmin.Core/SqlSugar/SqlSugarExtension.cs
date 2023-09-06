// PurestAdmin
// Author:dym
// MIT License
// https://gitee.com/dymproject/purest

using System.Data.SqlTypes;

using Furion.Logging.Extensions;

using Microsoft.Extensions.DependencyInjection;

using PurestAdmin.Core.Const;
using PurestAdmin.Core.Entity;

using Yitter.IdGenerator;

namespace PurestAdmin.Core.SqlSugar;
public static class SqlSugarExtension
{
    public static IServiceCollection AddSqlSugarService(this IServiceCollection services)
    {
        //List<Type> types = App.EffectiveTypes.Where(a => !a.IsAbstract && a.IsClass && a.GetCustomAttributes(typeof(SugarTable), true)?.FirstOrDefault() != null).ToList();
        SqlSugarScope sqlSugar = new(App.GetConfig<List<ConnectionConfig>>("ConnectionConfigs"),
            db =>
            {
                #region 查询过滤器

                #endregion

                #region 插入和更新过滤器
                db.Aop.DataExecuting = (oldValue, entityInfo) =>
                {
                    var userId = App.User?.FindFirst(ClaimConst.USERID)?.Value ?? "0";
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
                    exp.Sql.LogError<SqlString>();
                    //5.0.8.2 获取无参数化 SQL  对性能有影响，特别大的SQL参数多的，调试使用
                    //Console.WriteLine(UtilMethods.GetSqlString(DbType.Oracle, exp.Sql, exp.Parametres as SugarParameter[]));
                };
                #endregion
            });

        services.AddSingleton<ISqlSugarClient>(sqlSugar);
        services.AddScoped(typeof(Repository<>));
        return services;
    }

    public static IServiceCollection AddDictionaryDataService(this IServiceCollection services)
    {
        var _db = App.GetRequiredService<ISqlSugarClient>();
        //保证只配置一次不能更新,该配置是全局静态存储
        if (!_db.ConfigQuery.Any())
        {
            //字典
            _db.ConfigQuery.SetTable<DictDataEntity>(it => it.Id, it => it.Name);
            //组织机构
            _db.ConfigQuery.SetTable<OrganizationEntity>(it => it.Id, it => it.Name);
        }
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
