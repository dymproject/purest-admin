// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

using Microsoft.Extensions.DependencyInjection;

using Newtonsoft.Json;

using PurestAdmin.SqlSugar.Entity;
using PurestAdmin.Zero;

using SqlSugar;

using Volo.Abp;

var app = await AbpApplicationFactory.CreateAsync<ZeroModule>();
// 初始化应用
await app.InitializeAsync();

Console.WriteLine("获取db实例！");
var db = app.ServiceProvider.GetRequiredService<ISqlSugarClient>();

var funtions = db.Queryable<FunctionEntity>().ToList();
var json = JsonConvert.SerializeObject(funtions);
Console.WriteLine("开启事务！");
await db.Ado.BeginTranAsync();
var organization = new OrganizationEntity() { Name = "初始化组织机构", Leader = "dym" };
var organizationId = await db.Insertable(organization).ExecuteReturnSnowflakeIdAsync();

var user = new UserEntity()
{
    Account = "admin",
    Password = "e10adc3949ba59abbe56e057f20f883e",//123456
    Name = "dym",
    OrganizationId = organizationId
};
var uid = await db.Insertable(user).ExecuteReturnSnowflakeIdAsync();

var role = new RoleEntity { Name = "超级管理员" };
var roleId = await db.Insertable(role).ExecuteReturnSnowflakeIdAsync();

var userRole = new UserRoleEntity { RoleId = roleId, UserId = uid };
_ = await db.Insertable(userRole).ExecuteReturnSnowflakeIdAsync();

var funcWelcome = new FunctionEntity() { Name = "系统主页", Code = "welcome" };
_ = await db.Insertable(funcWelcome).ExecuteReturnSnowflakeIdAsync();

var funcSystem = new FunctionEntity() { Name = "系统管理", Code = "system" };


Console.ReadLine();