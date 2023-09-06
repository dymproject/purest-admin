// PurestAdmin
// Author:dym
// MIT License
// https://gitee.com/dymproject/purest

using Furion.Schedule;

using Microsoft.Extensions.DependencyInjection;

using PurestAdmin.Core.Job;
using PurestAdmin.Core.SnowFlake;
using PurestAdmin.Core.SqlSugar;

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
    }
}
