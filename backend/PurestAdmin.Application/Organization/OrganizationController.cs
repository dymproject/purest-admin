// PurestAdmin
// Author:dym
// MIT License
// https://gitee.com/dymproject/purest

using PurestAdmin.Application.Organization.Dtos;
using PurestAdmin.Application.Organization.Services;
using PurestAdmin.Core.Consts;

namespace PurestAdmin.Facade.Controllers;
/// <summary>
/// 组织机构接口
/// </summary>
[ApiDescriptionSettings(ApiGroupConst.SYSTEM)]
public class OrganizationController : IDynamicApiController
{
    private readonly IOrganizationService _organizationService;
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="organizationService"></param>
    public OrganizationController(IOrganizationService organizationService)
    {
        _organizationService = organizationService;
    }

    /// <summary>
    /// 分页查询
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpGet, ApiDescriptionSettings(Name = "page")]
    public async Task<PagedList<OrganizationProfile>> GetPagedListAsync([FromQuery, Required] GetPagedListInput input)
    {
        return await _organizationService.GetPagedListAsync(input);
    }

    /// <summary>
    /// 单条查询
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<OrganizationProfile> GetAsync(long id)
    {
        return await _organizationService.GetAsync(id);
    }

    /// <summary>
    /// 添加
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<long> AddAsync(AddOrganizationInput input)
    {
        return await _organizationService.AddAsync(input);
    }

    /// <summary>
    /// 编辑
    /// </summary>
    /// <param name="id"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPut]
    public async Task PutAsync(long id, AddOrganizationInput input)
    {
        await _organizationService.PutAsync(id, input);
    }

    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete]
    public async Task DeleteAsync(long id)
    {
        await _organizationService.DeleteAsync(id);
    }
}
