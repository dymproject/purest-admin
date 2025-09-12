// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

using PurestAdmin.SqlSugar.Entity;

using SqlSugar;

using Volo.Abp.DependencyInjection;

using Yitter.IdGenerator;
namespace PurestAdmin.Zero;
public class DataSeed : ISingletonDependency
{
    private readonly ISqlSugarClient _db;
    public DataSeed(ISqlSugarClient db)
    {
        _db = db;
    }
    public async Task Initialization()
    {
        Console.WriteLine("种子数据初始化准备就绪");
        try
        {
            Console.WriteLine("开启事务！");
            await _db.Ado.BeginTranAsync();
            var organization = new OrganizationEntity() { Name = "PurestAdmin", Leader = "dym" };
            var organizationId = await _db.Insertable(organization).ExecuteReturnSnowflakeIdAsync();
            var lv1_1 = await _db.Insertable(new OrganizationEntity() { Name = "人力资源", Leader = "dym", ParentId = organizationId }).ExecuteReturnSnowflakeIdAsync();
            var lv1_2 = await _db.Insertable(new OrganizationEntity() { Name = "软件研发", Leader = "dym", ParentId = organizationId }).ExecuteReturnSnowflakeIdAsync();
            var lv2_1 = await _db.Insertable(new OrganizationEntity() { Name = "研发主管", Leader = "dym", ParentId = lv1_2 }).ExecuteReturnSnowflakeIdAsync();
            Console.WriteLine("初始化组织机构完成");
            var user = new UserEntity()
            {
                Account = "admin",
                Password = "e10adc3949ba59abbe56e057f20f883e",//123456
                Name = "dym",
                OrganizationId = organizationId
            };
            var uid = await _db.Insertable(user).ExecuteReturnSnowflakeIdAsync();

            var uid1 = await _db.Insertable(new UserEntity() { Account = "rlzy1", Password = "e10adc3949ba59abbe56e057f20f883e", Name = "人力资源1", OrganizationId = lv1_1 }).ExecuteReturnSnowflakeIdAsync();
            var uid2 = await _db.Insertable(new UserEntity() { Account = "rlzy2", Password = "e10adc3949ba59abbe56e057f20f883e", Name = "人力资源2", OrganizationId = lv1_1 }).ExecuteReturnSnowflakeIdAsync();
            var uid3 = await _db.Insertable(new UserEntity() { Account = "yfzg1", Password = "e10adc3949ba59abbe56e057f20f883e", Name = "研发主管1", OrganizationId = lv2_1 }).ExecuteReturnSnowflakeIdAsync();
            var uid4 = await _db.Insertable(new UserEntity() { Account = "yfzg2", Password = "e10adc3949ba59abbe56e057f20f883e", Name = "研发主管2", OrganizationId = lv2_1 }).ExecuteReturnSnowflakeIdAsync();

            Console.WriteLine("初始化用户admin/123456");
            var role = new RoleEntity { Name = "超级管理员" };
            var roleId = await _db.Insertable(role).ExecuteReturnSnowflakeIdAsync();
            Console.WriteLine("初始化角色：超级管理员");
            UserRoleEntity[] userRoles = [
                new UserRoleEntity { RoleId = roleId, UserId = uid },
                new UserRoleEntity { RoleId = roleId, UserId = uid1 },
                new UserRoleEntity { RoleId = roleId, UserId = uid2 },
                new UserRoleEntity { RoleId = roleId, UserId = uid3 },
                new UserRoleEntity { RoleId = roleId, UserId = uid4 }
            ];
            _ = await _db.Insertable(userRoles).ExecuteReturnSnowflakeIdListAsync();
            Console.WriteLine("绑定用户角色关系admin->超级管理员");
            var dashboardId = YitIdHelper.NextId();
            var welcomeId = YitIdHelper.NextId();
            var systemId = YitIdHelper.NextId();
            var systemOrganizationId = YitIdHelper.NextId();
            var systemUserId = YitIdHelper.NextId();
            var systemRoleId = YitIdHelper.NextId();
            var systemFunctionId = YitIdHelper.NextId();
            var systemDictionaryId = YitIdHelper.NextId();
            var systemConfigId = YitIdHelper.NextId();
            var systemOnlineUserId = YitIdHelper.NextId();
            var systemRequestLogId = YitIdHelper.NextId();
            var systemNoticeId = YitIdHelper.NextId();
            var systemProfileSytemId = YitIdHelper.NextId();
            var workflowId = YitIdHelper.NextId();
            var definitionId = YitIdHelper.NextId();
            var myInstanceId = YitIdHelper.NextId();
            var waitingAuditingId = YitIdHelper.NextId();
            List<FunctionEntity> functions = [
                new FunctionEntity() { Id = dashboardId, Name = "欢迎使用", Code = "dashboard",Remark = "此项主要是为了控制首页显示内容有个归属" },
                new FunctionEntity() { Id = welcomeId, Name = "系统主页", ParentId = dashboardId, Code = "welcome",Remark = "前端未控制此功能权限,一定会显示" },
                new FunctionEntity() { Id = systemOnlineUserId, Name = "在线用户", ParentId = dashboardId, Code = "system.onlineuser" },
                new FunctionEntity() { Id = YitIdHelper.NextId(), ParentId = systemOnlineUserId, Name = "强制下线", Code = "system.onlineuser.logout" },
                new FunctionEntity() { Id = YitIdHelper.NextId(), ParentId = systemOnlineUserId, Name = "发送消息", Code = "system.onlineuser.sendmsg" },
                new FunctionEntity() { Id = systemId, Name = "系统管理", Code = "system" },
                new FunctionEntity() { Id = systemOrganizationId, ParentId = systemId, Name = "组织机构", Code = "system.organization" },
                new FunctionEntity() { Id = YitIdHelper.NextId(), ParentId = systemOrganizationId, Name = "组织机构新增", Code = "system.organization.add" },
                new FunctionEntity() { Id = YitIdHelper.NextId(), ParentId = systemOrganizationId, Name = "组织机构编辑", Code = "system.organization.edit" },
                new FunctionEntity() { Id = YitIdHelper.NextId(), ParentId = systemOrganizationId, Name = "组织机构查看", Code = "system.organization.view" },
                new FunctionEntity() { Id = YitIdHelper.NextId(), ParentId = systemOrganizationId, Name = "组织机构删除", Code = "system.organization.delete" },
                new FunctionEntity() { Id = systemUserId, ParentId = systemId, Name = "用户管理", Code = "system.user" },
                new FunctionEntity() { Id = YitIdHelper.NextId(), ParentId = systemUserId, Name = "用户新增", Code = "system.user.add" },
                new FunctionEntity() { Id = YitIdHelper.NextId(), ParentId = systemUserId, Name = "用户编辑", Code = "system.user.edit" },
                new FunctionEntity() { Id = YitIdHelper.NextId(), ParentId = systemUserId, Name = "用户查看", Code = "system.user.view" },
                new FunctionEntity() { Id = YitIdHelper.NextId(), ParentId = systemUserId, Name = "用户删除", Code = "system.user.delete" },
                new FunctionEntity() { Id = YitIdHelper.NextId(), ParentId = systemUserId, Name = "重置密码", Code = "system.user.resetpassword" },
                new FunctionEntity() { Id = YitIdHelper.NextId(), ParentId = systemUserId, Name = "用户状态", Code = "system.user.status" },
                new FunctionEntity() { Id = systemRoleId, ParentId = systemId, Name = "角色管理", Code = "system.role" },
                new FunctionEntity() { Id = YitIdHelper.NextId(), ParentId = systemRoleId, Name = "角色新增", Code = "system.role.add" },
                new FunctionEntity() { Id = YitIdHelper.NextId(), ParentId = systemRoleId, Name = "角色编辑", Code = "system.role.edit" },
                new FunctionEntity() { Id = YitIdHelper.NextId(), ParentId = systemRoleId, Name = "角色查看", Code = "system.role.view" },
                new FunctionEntity() { Id = YitIdHelper.NextId(), ParentId = systemRoleId, Name = "角色删除", Code = "system.role.delete" },
                new FunctionEntity() { Id = YitIdHelper.NextId(), ParentId = systemRoleId, Name = "页面权限", Code = "system.role.pagesecurity" },
                new FunctionEntity() { Id = YitIdHelper.NextId(), ParentId = systemRoleId, Name = "接口权限", Code = "system.role.interfacesecurity" },
                new FunctionEntity() { Id = systemFunctionId, ParentId = systemId, Name = "功能管理", Code = "system.function" },
                new FunctionEntity() { Id = YitIdHelper.NextId(), ParentId = systemFunctionId, Name = "功能新增", Code = "system.function.add" },
                new FunctionEntity() { Id = YitIdHelper.NextId(), ParentId = systemFunctionId, Name = "功能编辑", Code = "system.function.edit" },
                new FunctionEntity() { Id = YitIdHelper.NextId(), ParentId = systemFunctionId, Name = "功能查看", Code = "system.function.view" },
                new FunctionEntity() { Id = YitIdHelper.NextId(), ParentId = systemFunctionId, Name = "功能删除", Code = "system.function.delete" },
                new FunctionEntity() { Id = YitIdHelper.NextId(), ParentId = systemFunctionId, Name = "绑定接口", Code = "system.function.bind" },
                new FunctionEntity() { Id = YitIdHelper.NextId(), ParentId = systemFunctionId, Name = "解除绑定", Code = "system.function.unbind" },
                new FunctionEntity() { Id = YitIdHelper.NextId(), ParentId = systemFunctionId, Name = "同步接口", Code = "system.function.synchronization" },
                new FunctionEntity() { Id = systemDictionaryId, ParentId = systemId, Name = "字典管理", Code = "system.dictionary" },
                new FunctionEntity() { Id = YitIdHelper.NextId(), ParentId = systemDictionaryId, Name = "字典新增", Code = "system.dictionary.add" },
                new FunctionEntity() { Id = YitIdHelper.NextId(), ParentId = systemDictionaryId, Name = "字典编辑", Code = "system.dictionary.edit" },
                new FunctionEntity() { Id = YitIdHelper.NextId(), ParentId = systemDictionaryId, Name = "字典查看", Code = "system.dictionary.view" },
                new FunctionEntity() { Id = YitIdHelper.NextId(), ParentId = systemDictionaryId, Name = "字典删除", Code = "system.dictionary.delete" },
                new FunctionEntity() { Id = systemConfigId, ParentId = systemId, Name = "系统配置管理", Code = "system.systemconfig" },
                new FunctionEntity() { Id = YitIdHelper.NextId(), ParentId = systemConfigId, Name = "系统配置新增", Code = "system.systemconfig.add" },
                new FunctionEntity() { Id = YitIdHelper.NextId(), ParentId = systemConfigId, Name = "系统配置编辑", Code = "system.systemconfig.edit" },
                new FunctionEntity() { Id = YitIdHelper.NextId(), ParentId = systemConfigId, Name = "系统配置查看", Code = "system.systemconfig.view" },
                new FunctionEntity() { Id = YitIdHelper.NextId(), ParentId = systemConfigId, Name = "系统配置删除", Code = "system.systemconfig.delete" },
                new FunctionEntity() { Id = systemRequestLogId, ParentId = systemId, Name = "请求日志", Code = "system.requestlog" },
                new FunctionEntity() { Id = systemNoticeId, ParentId = systemId, Name = "通知公告", Code = "system.notice" },
                new FunctionEntity() { Id = YitIdHelper.NextId(), ParentId = systemNoticeId, Name = "通知公告新增", Code = "system.notice.add" },
                new FunctionEntity() { Id = YitIdHelper.NextId(), ParentId = systemNoticeId, Name = "通知公告编辑", Code = "system.notice.edit" },
                new FunctionEntity() { Id = YitIdHelper.NextId(), ParentId = systemNoticeId, Name = "通知公告查看", Code = "system.notice.view" },
                new FunctionEntity() { Id = YitIdHelper.NextId(), ParentId = systemNoticeId, Name = "通知公告删除", Code = "system.notice.delete" },
                new FunctionEntity() { Id = systemProfileSytemId, ParentId = systemId, Name = "系统文件", Code = "system.profilesystem" },
                new FunctionEntity() { Id = YitIdHelper.NextId(), ParentId = systemProfileSytemId, Name = "系统文件新增", Code = "system.profilesystem.add" },
                new FunctionEntity() { Id = YitIdHelper.NextId(), ParentId = systemProfileSytemId, Name = "系统文件删除", Code = "system.profilesystem.delete" },
                new FunctionEntity() { Id = workflowId, Name = "工作流程", Code = "workflow" },
                new FunctionEntity() { Id = definitionId, ParentId = workflowId, Name = "流程模版", Code = "workflow.definition" },
                new FunctionEntity() { Id = YitIdHelper.NextId(), ParentId = definitionId, Name = "模版新增", Code = "workflow.definition.add" },
                new FunctionEntity() { Id = YitIdHelper.NextId(), ParentId = definitionId, Name = "模版编辑", Code = "workflow.definition.edit" },
                new FunctionEntity() { Id = YitIdHelper.NextId(), ParentId = definitionId, Name = "模版查看", Code = "workflow.definition.view" },
                new FunctionEntity() { Id = YitIdHelper.NextId(), ParentId = definitionId, Name = "模版删除", Code = "workflow.definition.delete" },
                new FunctionEntity() { Id = YitIdHelper.NextId(), ParentId = definitionId, Name = "模版锁定", Code = "workflow.definition.lock" },
                new FunctionEntity() { Id = myInstanceId, ParentId = workflowId, Name = "我的流程", Code = "workflow.my" },
                new FunctionEntity() { Id = YitIdHelper.NextId(), ParentId = myInstanceId, Name = "发起流程", Code = "workflow.my.add" },
                new FunctionEntity() { Id = YitIdHelper.NextId(), ParentId = myInstanceId, Name = "详细信息", Code = "workflow.my.view" },
                new FunctionEntity() { Id = waitingAuditingId, ParentId = workflowId, Name = "待办事项", Code = "workflow.auditing" },
                new FunctionEntity() { Id = YitIdHelper.NextId(), ParentId = waitingAuditingId, Name = "流程审批", Code = "workflow.auditing.approve" },
            ];
            await _db.Insertable(functions).ExecuteCommandAsync();
            Console.WriteLine("初始化功能数据");

            var roleFunctions = functions.Select(x => new RoleFunctionEntity() { FunctionId = x.Id, RoleId = roleId }).ToList();
            await _db.Insertable(roleFunctions).ExecuteReturnSnowflakeIdListAsync();

            Console.WriteLine("绑定角色功能数据");
            var dictSexId = YitIdHelper.NextId();
            var dictNoticeLevelId = YitIdHelper.NextId();
            var dictNoticeTypeId = YitIdHelper.NextId();
            DictCategoryEntity[] categorys = [
                new DictCategoryEntity() { Id = dictSexId, Code = "dict_sex", Name = "性别" },
                new DictCategoryEntity() { Id = dictNoticeTypeId, Code = "dict_notice_type", Name = "通知类型" },
                new DictCategoryEntity() { Id = dictNoticeLevelId, Code = "dict_notice_level", Name = "通知级别" },
            ];
            await _db.Insertable(categorys).ExecuteCommandAsync();
            Console.WriteLine("初始化字典分类数据");

            DictDataEntity[] dictDatas = [
                new DictDataEntity() { CategoryId = dictSexId, Name = "男",Code = "male" },
                new DictDataEntity() { CategoryId = dictSexId, Name = "女",Code = "female" },

                new DictDataEntity() { CategoryId = dictNoticeTypeId, Name = "消息", Code = "msg"},
                new DictDataEntity() { CategoryId = dictNoticeTypeId, Name = "公告", Code = "notice"},
                new DictDataEntity() { CategoryId = dictNoticeTypeId, Name = "待办", Code = "todo"},

                new DictDataEntity() { CategoryId = dictNoticeLevelId, Name = "紧急", Code = "danger" },
                new DictDataEntity() { CategoryId = dictNoticeLevelId, Name = "警告", Code = "warning" },
                new DictDataEntity() { CategoryId = dictNoticeLevelId, Name = "正常", Code = "info"  }
                ];
            await _db.Insertable(dictDatas).ExecuteReturnSnowflakeIdListAsync();
            Console.WriteLine("初始化字典明细数据");
            _db.Ado.CommitTran();
            Console.WriteLine("初始化数据完成");
        }
        catch (Exception)
        {
            _db.Ado.RollbackTran();
            throw;
        }
        Console.ReadLine();
    }
}
