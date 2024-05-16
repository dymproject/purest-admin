// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

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
