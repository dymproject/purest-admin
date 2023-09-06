// PurestAdmin
// Author:dym
// MIT License
// https://gitee.com/dymproject/purest

using PurestAdmin.Application.Interface.Dtos;
using PurestAdmin.Application.Interface.Services;

namespace PurestAdmin.Facade.Controllers;
/// <summary>
/// 系统功能接口
/// </summary>
[ApiDescriptionSettings(ApiGroupConst.SYSTEM)]
public class InterfaceController : IDynamicApiController
{
    private readonly IInterfaceService _interfaceService;
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="interfaceService"></param>
    public InterfaceController(IInterfaceService interfaceService)
    {
        _interfaceService = interfaceService;
    }

    /// <summary>
    /// 分页查询
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpGet, ApiDescriptionSettings(Name = "page")]
    public async Task<PagedList<InterfaceGroupProfile>> GetPagedListAsync([FromQuery, Required] GetPagedListInput input)
    {
        return await _interfaceService.GetPagedListAsync(input);
    }

    /// <summary>
    /// 接口导入
    /// </summary>
    /// <param name="file"></param>
    /// <returns></returns>
    [HttpPost, UnitOfWork, ApiDescriptionSettings(Name = "import")]
    public async Task<int> ImportInterfaceAsync([Required] IFormFile file)
    {
        return await _interfaceService.ImportInterfaceAsync(file);
    }

}
