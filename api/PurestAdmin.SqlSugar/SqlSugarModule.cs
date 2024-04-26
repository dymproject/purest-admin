// MIT 许可证
// 版权 © 2023-present https://github.com/dymproject/purest-admin作者 以及贡献者
// 作者或版权持有人都不对任何索赔、损害或其他责任负责，无论这些追责来自合同、侵权或其它行为中，
// 还是产生于、源于或有关于本软件以及本软件的使用或其它处置。

using Microsoft.Extensions.DependencyInjection;

using PurestAdmin.SqlSugar.Entity;

using Volo.Abp;
using Volo.Abp.Modularity;

namespace PurestAdmin.SqlSugar
{
    public class SqlSugarModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddSqlSugarService();
            context.Services.ReplaceSqlSugarSnowflakeIdService();
        }
        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var _db = context.ServiceProvider.GetRequiredService<ISqlSugarClient>();
            //保证只配置一次不能更新,该配置是全局静态存储
            if (!_db.ConfigQuery.Any())
            {
                //字典
                _db.ConfigQuery.SetTable<DictDataEntity>(it => it.Id, it => it.Name);
                ////组织机构
                _db.ConfigQuery.SetTable<OrganizationEntity>(it => it.Id, it => it.Name);
            }
        }
    }
}
