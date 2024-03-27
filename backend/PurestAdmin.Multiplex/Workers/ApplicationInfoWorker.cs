// MIT 许可证
// 版权 © 2023-present https://github.com/dymproject/purest-admin作者 以及贡献者
// 作者或版权持有人都不对任何索赔、损害或其他责任负责，无论这些追责来自合同、侵权或其它行为中，
// 还是产生于、源于或有关于本软件以及本软件的使用或其它处置。

using Microsoft.Extensions.DependencyInjection;

using Volo.Abp.BackgroundWorkers;
using Volo.Abp.Threading;

namespace PurestAdmin.Multiplex.Workers;

public class ApplicationInfoWorker : AsyncPeriodicBackgroundWorkerBase
{
    public ApplicationInfoWorker(AbpAsyncTimer timer, IServiceScopeFactory serviceScopeFactory) : base(timer, serviceScopeFactory)
    {
        Timer.Period = 10000;
    }

    protected override async Task DoWorkAsync(PeriodicBackgroundWorkerContext workerContext)
    {
        //var hubContext = workerContext.ServiceProvider.GetRequiredService<IHubContext<SystemPlatformHub, ISystemPlatformClient>>();
        //await Task.Run(() =>
        //{
        //    hubContext.Clients.All.SystemPlatform(new SystemPlatformInfo());
        //});
    }
}