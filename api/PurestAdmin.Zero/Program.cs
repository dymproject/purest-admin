// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

using Microsoft.Extensions.DependencyInjection;

using PurestAdmin.SqlSugar.Entity;
using PurestAdmin.Zero;

using SqlSugar;

using Volo.Abp;

using Yitter.IdGenerator;

var app = await AbpApplicationFactory.CreateAsync<ZeroModule>();
// 初始化应用
await app.InitializeAsync();
Console.WriteLine("种子数据初始化准备就绪");
Console.WriteLine("获取db实例！");
var db = app.ServiceProvider.GetRequiredService<ISqlSugarClient>();
try
{
    Console.WriteLine("开启事务！");
    await db.Ado.BeginTranAsync();
    var organization = new OrganizationEntity() { Name = "初始化组织机构", Leader = "dym" };
    var organizationId = await db.Insertable(organization).ExecuteReturnSnowflakeIdAsync();
    Console.WriteLine("初始化组织机构完成");
    var user = new UserEntity()
    {
        Account = "admin",
        Password = "e10adc3949ba59abbe56e057f20f883e",//123456
        Name = "dym",
        OrganizationId = organizationId
    };
    var uid = await db.Insertable(user).ExecuteReturnSnowflakeIdAsync();
    Console.WriteLine("初始化用户admin/123456");
    var role = new RoleEntity { Name = "超级管理员" };
    var roleId = await db.Insertable(role).ExecuteReturnSnowflakeIdAsync();
    Console.WriteLine("初始化角色：超级管理员");
    var userRole = new UserRoleEntity { RoleId = roleId, UserId = uid };
    _ = await db.Insertable(userRole).ExecuteReturnSnowflakeIdAsync();
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
    ];
    await db.Insertable(functions).ExecuteCommandAsync();
    Console.WriteLine("初始化功能数据");

    var roleFunctions = functions.Select(x => new RoleFunctionEntity() { FunctionId = x.Id, RoleId = roleId }).ToList();
    await db.Insertable(roleFunctions).ExecuteReturnSnowflakeIdListAsync();

    Console.WriteLine("绑定角色功能数据");
    var dictSexId = YitIdHelper.NextId();
    var dictNoticeLevelId = YitIdHelper.NextId();
    var dictNoticeTypeId = YitIdHelper.NextId();
    DictCategoryEntity[] categorys = [
        new DictCategoryEntity() { Id = dictSexId, Code = "dict_sex", Name = "性别" },
    new DictCategoryEntity() { Id = dictNoticeTypeId, Code = "dict_notice_type", Name = "通知类型" },
    new DictCategoryEntity() { Id = dictNoticeLevelId, Code = "dict_notice_level", Name = "通知级别" },
    ];
    await db.Insertable(categorys).ExecuteCommandAsync();
    Console.WriteLine("初始化字典分类数据");

    DictDataEntity[] dictDatas = [
        new DictDataEntity() { CategoryId = dictSexId, Name = "男" },
        new DictDataEntity() { CategoryId = dictSexId, Name = "女" },

        new DictDataEntity() { CategoryId = dictNoticeTypeId, Name = "消息" },
        new DictDataEntity() { CategoryId = dictNoticeTypeId, Name = "公告" },
        new DictDataEntity() { CategoryId = dictNoticeTypeId, Name = "待办" },

        new DictDataEntity() { CategoryId = dictNoticeLevelId, Name = "紧急", Remark = "danger" },
        new DictDataEntity() { CategoryId = dictNoticeLevelId, Name = "警告", Remark = "warning" },
        new DictDataEntity() { CategoryId = dictNoticeLevelId, Name = "正常", Remark = "info"  }
        ];
    await db.Insertable(dictDatas).ExecuteReturnSnowflakeIdListAsync();
    Console.WriteLine("初始化字典明细数据");
    db.Ado.CommitTran();
    Console.WriteLine("初始化数据完成");
}
catch (Exception)
{
    db.Ado.RollbackTran();
    throw;
}
Console.ReadLine();