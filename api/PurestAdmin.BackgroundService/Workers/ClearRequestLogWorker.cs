// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

using Microsoft.Extensions.DependencyInjection;

using PurestAdmin.SqlSugar.Entity;

using SqlSugar;

using Volo.Abp.BackgroundWorkers;
using Volo.Abp.Threading;
using Volo.Abp.Timing;

namespace PurestAdmin.BackgroundService.Workers;

public class ClearRequestLogWorker : AsyncPeriodicBackgroundWorkerBase
{
    public ClearRequestLogWorker(AbpAsyncTimer timer, IServiceScopeFactory serviceScopeFactory) : base(timer, serviceScopeFactory)
    {
        Timer.Period = 1000 * 60 * 60 * 24 * 14;
    }

    protected override async Task DoWorkAsync(PeriodicBackgroundWorkerContext workerContext)
    {
        using var scope = workerContext.ServiceProvider.CreateScope();
        var db = scope.ServiceProvider.GetService<ISqlSugarClient>() ?? throw new Exception();
        var clock = scope.ServiceProvider.GetService<IClock>() ?? throw new Exception();
        _ = await db.Deleteable<RequestLogEntity>().Where(x => x.CreateTime <= clock.Now.AddDays(-14)).ExecuteCommandAsync();
    }
}