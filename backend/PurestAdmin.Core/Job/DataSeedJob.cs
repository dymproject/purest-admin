// PurestAdmin
// Author:dym
// MIT License
// https://gitee.com/dymproject/purest

using Furion.Schedule;

using PurestAdmin.Core.Consts;
using PurestAdmin.Core.Entity;

namespace PurestAdmin.Core.Job;
/// <summary>
/// 种子数据初始化job
/// </summary>
/// <remarks>午夜开始</remarks>
//[Daily(RunOnStart = true)]
[PeriodSeconds(3, MaxNumberOfRuns = 1)]
[JobDetail("DataSeedJob", "种子数据初始化job")]
public class DataSeedJob : IJob
{
    public async Task ExecuteAsync(JobExecutingContext context, CancellationToken stoppingToken)
    {
        var _db = App.GetRequiredService<ISqlSugarClient>();
        try
        {
            _db.Ado.BeginTran();
            //清理数据
            await _db.Deleteable<UserRoleEntity>().ExecuteCommandAsync(stoppingToken);
            await _db.Deleteable<RoleEntity>().ExecuteCommandAsync(stoppingToken);
            await _db.Deleteable<UserEntity>().ExecuteCommandAsync(stoppingToken);
            await _db.Deleteable<OrganizationEntity>().ExecuteCommandAsync(stoppingToken);
            await _db.Deleteable<DictDataEntity>().ExecuteCommandAsync(stoppingToken);
            await _db.Deleteable<DictCategoryEntity>().ExecuteCommandAsync(stoppingToken);
            //组织机构
            var organization = new OrganizationEntity { Name = "初始化组织机构", Leader = "dym", };
            var organizationId = await _db.Insertable(organization).ExecuteReturnSnowflakeIdAsync(stoppingToken);
            var user = new UserEntity
            {
                Account = "admin",
                Password = AESEncryption.Encrypt("123456", AesKeyConst.Key),
                Name = "dym",
                OrganizationId = organizationId
            };
            //用户
            var userId = await _db.Insertable(user).ExecuteReturnSnowflakeIdAsync(stoppingToken);
            //角色
            var role = new RoleEntity { Name = "超级管理员" };
            var roleId = await _db.Insertable(role).ExecuteReturnSnowflakeIdAsync(stoppingToken);
            //用户角色关系
            var userRole = new UserRoleEntity { RoleId = roleId, UserId = userId };
            await _db.Insertable(userRole).ExecuteReturnSnowflakeIdAsync(stoppingToken);
            //前端功能
            var (listFunction, parentIds) = (new List<FunctionEntity>(), new List<long>());
            var firstLevelId = await _db.Insertable(new FunctionEntity { Name = "系统管理", Code = "system" }).ExecuteReturnSnowflakeIdAsync(stoppingToken);
            parentIds.Add(firstLevelId);
            var secondLevelId = await _db.Insertable(new FunctionEntity { Name = "组织机构", Code = "system.organization", ParentId = firstLevelId }).ExecuteReturnSnowflakeIdAsync(stoppingToken);
            parentIds.Add(secondLevelId);
            listFunction.Add(new FunctionEntity { ParentId = secondLevelId, Name = "组织机构新增", Code = "system.organization.add" });
            listFunction.Add(new FunctionEntity { ParentId = secondLevelId, Name = "组织机构编辑", Code = "system.organization.edit" });
            listFunction.Add(new FunctionEntity { ParentId = secondLevelId, Name = "组织机构查看", Code = "system.organization.view" });
            listFunction.Add(new FunctionEntity { ParentId = secondLevelId, Name = "组织机构删除", Code = "system.organization.delete" });

            secondLevelId = await _db.Insertable(new FunctionEntity { Name = "用户管理", Code = "system.user", ParentId = firstLevelId }).ExecuteReturnSnowflakeIdAsync(stoppingToken);
            parentIds.Add(secondLevelId);
            listFunction.Add(new FunctionEntity { ParentId = secondLevelId, Name = "用户新增", Code = "system.user.add" });
            listFunction.Add(new FunctionEntity { ParentId = secondLevelId, Name = "用户编辑", Code = "system.user.edit" });
            listFunction.Add(new FunctionEntity { ParentId = secondLevelId, Name = "用户查看", Code = "system.user.view" });
            listFunction.Add(new FunctionEntity { ParentId = secondLevelId, Name = "用户删除", Code = "system.user.delete" });
            listFunction.Add(new FunctionEntity { ParentId = secondLevelId, Name = "重置密码", Code = "system.user.resetpassword" });

            secondLevelId = await _db.Insertable(new FunctionEntity { Name = "角色管理", Code = "system.role", ParentId = firstLevelId }).ExecuteReturnSnowflakeIdAsync(stoppingToken);
            parentIds.Add(secondLevelId);
            listFunction.Add(new FunctionEntity { ParentId = secondLevelId, Name = "角色新增", Code = "system.role.add" });
            listFunction.Add(new FunctionEntity { ParentId = secondLevelId, Name = "角色编辑", Code = "system.role.edit" });
            listFunction.Add(new FunctionEntity { ParentId = secondLevelId, Name = "角色查看", Code = "system.role.view" });
            listFunction.Add(new FunctionEntity { ParentId = secondLevelId, Name = "角色删除", Code = "system.role.delete" });
            listFunction.Add(new FunctionEntity { ParentId = secondLevelId, Name = "绑定功能", Code = "system.role.bindfunction" });

            secondLevelId = await _db.Insertable(new FunctionEntity { Name = "功能管理", Code = "system.function", ParentId = firstLevelId }).ExecuteReturnSnowflakeIdAsync(stoppingToken);
            parentIds.Add(secondLevelId);
            listFunction.Add(new FunctionEntity { ParentId = secondLevelId, Name = "功能新增", Code = "system.function.add" });
            listFunction.Add(new FunctionEntity { ParentId = secondLevelId, Name = "功能编辑", Code = "system.function.edit" });
            listFunction.Add(new FunctionEntity { ParentId = secondLevelId, Name = "功能查看", Code = "system.function.view" });
            listFunction.Add(new FunctionEntity { ParentId = secondLevelId, Name = "功能删除", Code = "system.function.delete" });
            listFunction.Add(new FunctionEntity { ParentId = secondLevelId, Name = "绑定接口", Code = "system.function.bindinterface" });

            secondLevelId = await _db.Insertable(new FunctionEntity { Name = "字典管理", Code = "system.dictionary", ParentId = firstLevelId }).ExecuteReturnSnowflakeIdAsync(stoppingToken);
            parentIds.Add(secondLevelId);
            listFunction.Add(new FunctionEntity { ParentId = secondLevelId, Name = "字典新增", Code = "system.dictionary.add" });
            listFunction.Add(new FunctionEntity { ParentId = secondLevelId, Name = "字典编辑", Code = "system.dictionary.edit" });
            listFunction.Add(new FunctionEntity { ParentId = secondLevelId, Name = "字典查看", Code = "system.dictionary.view" });
            listFunction.Add(new FunctionEntity { ParentId = secondLevelId, Name = "字典删除", Code = "system.dictionary.delete" });

            secondLevelId = await _db.Insertable(new FunctionEntity { Name = "系统配置", Code = "system.systemconfig", ParentId = firstLevelId }).ExecuteReturnSnowflakeIdAsync(stoppingToken);
            parentIds.Add(secondLevelId);
            listFunction.Add(new FunctionEntity { ParentId = secondLevelId, Name = "系统配置新增", Code = "system.systemconfig.add" });
            listFunction.Add(new FunctionEntity { ParentId = secondLevelId, Name = "系统配置编辑", Code = "system.systemconfig.edit" });
            listFunction.Add(new FunctionEntity { ParentId = secondLevelId, Name = "系统配置查看", Code = "system.systemconfig.view" });
            listFunction.Add(new FunctionEntity { ParentId = secondLevelId, Name = "系统配置删除", Code = "system.systemconfig.delete" });

            var ids = await _db.Insertable(listFunction).ExecuteReturnSnowflakeIdListAsync(stoppingToken);
            //角色功能关系
            var rfEntities = ids.Concat(parentIds).ToList().Select(x => new RoleFunctionEntity() { RoleId = roleId, FunctionId = x }).ToList();
            _ = await _db.Insertable(rfEntities).ExecuteReturnSnowflakeIdListAsync(stoppingToken);

            _db.Ado.CommitTran();
        }
        catch (Exception)
        {
            _db.Ado.RollbackTran();
            throw;
        }
        await Task.CompletedTask;
    }
}
