// MIT 许可证
// 版权 © 2023-present dym 和所有贡献者
// 作者或版权持有人都不对任何索赔、损害或其他责任负责，无论这些追责来自合同、侵权或其它行为中，
// 还是产生于、源于或有关于本软件以及本软件的使用或其它处置。

using PurestAdmin.Application.SystemConfig.Dtos;
using PurestAdmin.Application.SystemConfig.Services;

namespace PurestAdmin.Application.SystemConfig;
/// <summary>
/// 系统配置接口
/// </summary>
[ApiDescriptionSettings(ApiGroupConst.SYSTEM, Description = "系统配置接口")]
public class SystemConfigController : IDynamicApiController
{
    private readonly ISystemConfigService _systemConfigService;
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="systemConfigService"></param>
    public SystemConfigController(ISystemConfigService systemConfigService)
    {
        _systemConfigService = systemConfigService;
    }

    /// <summary>
    /// 分页查询
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpGet, ApiDescriptionSettings(Name = "page")]
    public async Task<PagedList<SystemConfigProfile>> GetPagedListAsync([FromQuery, Required] GetPagedListInput input)
    {
        return await _systemConfigService.GetPagedListAsync(input);
    }

    /// <summary>
    /// 单条查询
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<SystemConfigProfile> GetAsync(long id)
    {
        return await _systemConfigService.GetAsync(id) ?? throw Oops.Bah(ErrorTipsEnum.NoResult);
    }

    /// <summary>
    /// 添加
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost, UnitOfWork]
    public async Task<long> AddAsync(AddSystemConfigInput input)
    {
        return await _systemConfigService.AddAsync(input);
    }

    /// <summary>
    /// 编辑
    /// </summary>
    /// <param name="id"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPut, UnitOfWork]
    public async Task PutAsync(long id, AddSystemConfigInput input)
    {
        await _systemConfigService.PutAsync(id, input);
    }

    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete, UnitOfWork]
    public async Task DeleteAsync(long id)
    {
        await _systemConfigService.DeleteAsync(id);
    }
}
