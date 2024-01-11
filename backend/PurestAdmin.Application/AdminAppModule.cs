// MIT 许可证
// 版权 © 2023-present https://github.com/dymproject/purest-admin作者 以及贡献者
// 作者或版权持有人都不对任何索赔、损害或其他责任负责，无论这些追责来自合同、侵权或其它行为中，
// 还是产生于、源于或有关于本软件以及本软件的使用或其它处置。

using PurestAdmin.Core.Attributes;

using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Modularity;

namespace PurestAdmin.Application
{
    public class AdminAppModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpAspNetCoreMvcOptions>(options =>
            {
                options.ConventionalControllers.Create(typeof(AdminAppModule).Assembly, opts =>
                {
                    opts.RootPath = "v1";
                    opts.UrlActionNameNormalizer = (action) =>
                    {
                        var attr = action.Action.Attributes.FirstOrDefault(x => x.GetType().Equals(typeof(ApiDescriptionSettingsAttribute))) as ApiDescriptionSettingsAttribute;
                        return attr?.Name ?? action.ActionNameInUrl;
                    };
                });
            });
            base.ConfigureServices(context);
        }
    }
}
