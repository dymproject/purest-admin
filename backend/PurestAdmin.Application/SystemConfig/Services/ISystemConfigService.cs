// PurestAdmin
// Author:dym
// MIT License
// https://gitee.com/dymproject/purest

using PurestAdmin.Application.SystemConfig.Dtos;

namespace PurestAdmin.Application.SystemConfig.Services;
/// <summary>
/// 系统配置表接口
/// </summary>
public interface ISystemConfigService
{
    /// <summary>
    /// 分页查询
    /// </summary>
    Task<PagedList<SystemConfigProfile>> GetPagedListAsync(GetPagedListInput input);
    /// <summary>
    /// 单条查询
    /// </summary>
    Task<SystemConfigProfile> GetAsync(long id);
    /// <summary>
    /// 添加数据
    /// </summary>
    Task<long> AddAsync(AddSystemConfigInput input);
    /// <summary>
    /// 编辑数据
    /// </summary>
    Task PutAsync(long id, AddSystemConfigInput input);
    /// <summary>
    /// 删除数据
    /// </summary>
    Task DeleteAsync(long id);
}