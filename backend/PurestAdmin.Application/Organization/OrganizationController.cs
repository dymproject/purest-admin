// MIT 许可证
// 版权 © 2023-present dym 和所有贡献者
// 作者或版权持有人都不对任何索赔、损害或其他责任负责，无论这些追责来自合同、侵权或其它行为中，
// 还是产生于、源于或有关于本软件以及本软件的使用或其它处置。

using PurestAdmin.Application.Organization.Dtos;
using PurestAdmin.Application.Organization.Services;

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
