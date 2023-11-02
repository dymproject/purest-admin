// MIT 许可证
// 版权 © 2023-present dym 和所有贡献者
// 作者或版权持有人都不对任何索赔、损害或其他责任负责，无论这些追责来自合同、侵权或其它行为中，
// 还是产生于、源于或有关于本软件以及本软件的使用或其它处置。

using Microsoft.Extensions.DependencyInjection;

using PurestAdmin.Core.Extensions.Log;
using PurestAdmin.Core.Extensions.SnowFlake;
using PurestAdmin.Core.Extensions.SqlSugar;

namespace PurestAdmin.Core;
[AppStartup(888)]
public class Startup : AppStartup
{
    public void ConfigureServices(IServiceCollection services)
    {
        //短位雪花Id
        services.AddSnowflakeId();
        //SqlSugar
        services.AddSqlSugarService();
        services.AddDictionaryDataService();
        services.AddUnitOfWork<SqlSugarUnitOfWork>();
        //替换sqlsugar的雪花Id
        services.ReplaceSqlSugarSnowflakeIdService();

        //解决HTTP 请求作业System.Net.Http.IHttpClientFactory 错误 详见26.1.2.7
        services.AddHttpClient();
        //定时任务
        services.AddSchedule(options =>
        {
            // 批量新增
            //options.AddJob(App.EffectiveTypes.ScanToBuilders());

            //options.AddJob(typeof(DataSeedJob).ScanToBuilder());
        });
        //日志
        services.AddPurestAdminLogging();
    }
}
