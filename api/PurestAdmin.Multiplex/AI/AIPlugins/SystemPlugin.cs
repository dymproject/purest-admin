// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

using System.ComponentModel;
using System.Text.Json;

using Microsoft.SemanticKernel;

namespace PurestAdmin.Multiplex.AI.AIPlugins;

public class SystemPlugin(ISqlSugarClient db)
{
    /// <summary>
    /// db
    /// </summary>
    private readonly ISqlSugarClient _db = db;
    /// <summary>
    /// 新增角色
    /// </summary>
    /// <param name="name">角色名</param>
    /// <returns></returns>
    [KernelFunction("add_role"), Description("新增角色")]
    public async Task<long> AddRoleAsync([Description("角色名称")] string name)
    {
        var role = await _db.Queryable<RoleEntity>().FirstAsync(x => x.Name == name.Trim());
        if (role != null)
        {
            return role.Id;
        }
        return await _db.Insertable(new RoleEntity() { Name = name.Trim() }).ExecuteReturnSnowflakeIdAsync();
    }
    /// <summary>
    /// 获取所有用户
    /// </summary>
    /// <returns></returns>
    [KernelFunction("get_all_users"), Description("获取所有用户")]
    public async Task<List<UserEntity>> GetAllUsersAsync()
    {
        return await _db.Queryable<UserEntity>().ToListAsync();
    }
    /// <summary>
    /// 获取单个角色
    /// </summary>
    /// <returns></returns>
    [KernelFunction("get_role"), Description("获取单个角色")]
    public async Task<RoleEntity> GetRoleAsync(long? id)
    {
        return await _db.Queryable<RoleEntity>().FirstAsync(x => x.Id == id);
    }
    /// <summary>
    /// 获取所有角色
    /// </summary>
    /// <returns></returns>
    [KernelFunction("get_all_roles"), Description("获取所有角色")]
    public async Task<List<RoleEntity>> GetAllRolesAsync()
    {
        return await _db.Queryable<RoleEntity>().ToListAsync();
    }
    /// <summary>
    /// 获取所有组织机构
    /// </summary>
    /// <returns></returns>
    [KernelFunction("get_all_organizations"), Description("获取所有组织机构")]
    public async Task<List<OrganizationEntity>> GetAllOrganizationsAsync()
    {
        return await _db.Queryable<OrganizationEntity>().ToListAsync();
    }
}
