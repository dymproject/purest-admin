// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

using System.Reflection;

using PurestAdmin.BackgroundService;
using PurestAdmin.Core.Mapster;
using PurestAdmin.Multiplex;

using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Modularity;

namespace PurestAdmin.Application
{
    [DependsOn(typeof(AdminSqlSugarModule),
        typeof(AdminBackgroundModule),
        typeof(AdminMultiplexModule))]
    public class AdminAppModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddMapsterIRegister(Assembly.GetExecutingAssembly());
            Configure<AbpAspNetCoreMvcOptions>(options =>
            {
                options.ConventionalControllers.Create(typeof(AdminAppModule).Assembly, opts =>
                {
                    opts.RootPath = "v1";
                    //opts.UrlActionNameNormalizer = (action) =>
                    //{
                    //    return action.ActionNameInUrl;
                    //};
                });
            });
            base.ConfigureServices(context);
        }
    }
}
