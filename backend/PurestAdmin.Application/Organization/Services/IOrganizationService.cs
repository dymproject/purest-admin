// PurestAdmin
// Author:dym
// MIT License
// https://gitee.com/dymproject/purest

using PurestAdmin.Application.Organization.Dtos;

namespace PurestAdmin.Application.Organization.Services;
/// <summary>
/// 组织机构接口
/// </summary>
public interface IOrganizationService
{
    /// <summary>
    /// 分页查询
    /// </summary>
    Task<PagedList<OrganizationProfile>> GetPagedListAsync(GetPagedListInput input);
    /// <summary>
    /// 单条查询
    /// </summary>
    Task<OrganizationProfile> GetAsync(long id);
    /// <summary>
    /// 添加数据
    /// </summary>
    Task<long> AddAsync(AddOrganizationInput input);
    /// <summary>
    /// 编辑数据
    /// </summary>
    Task PutAsync(long id, AddOrganizationInput input);
    /// <summary>
    /// 删除数据
    /// </summary>
    Task DeleteAsync(long id);
}