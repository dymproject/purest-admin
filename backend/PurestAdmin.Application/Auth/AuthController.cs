// MIT 许可证
// 版权 © 2023-present dym 和所有贡献者
// 作者或版权持有人都不对任何索赔、损害或其他责任负责，无论这些追责来自合同、侵权或其它行为中，
// 还是产生于、源于或有关于本软件以及本软件的使用或其它处置。

using PurestAdmin.Application.Auth.Dtos;
using PurestAdmin.Application.Auth.Services;
using PurestAdmin.Application.Organization.Dtos;

namespace PurestAdmin.Facade.Controllers;
/// <summary>
/// 权限接口
/// </summary>
[ApiDescriptionSettings(ApiGroupConst.SYSTEM)]
public class AuthController : IDynamicApiController
{
    /// <summary>
    /// Auth接口
    /// </summary>
    private readonly IAuthService _authService;
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="authService"></param>
    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    /// <summary>
    /// 用户登录
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost, AllowAnonymous, NonUnify]
    public async Task<LoginOutput> LoginAsync(LoginInput input)
    {
        return await _authService.LoginAsync(input);
    }

    /// <summary>
    /// 获取当前用户权限信息
    /// </summary>
    /// <returns></returns>
    [HttpGet, ApiDescriptionSettings(Name = "userinfo")]
    public async Task<UserInfo> GetUserInfoAsync([FromQuery, Required] string password)
    {
        return await _authService.GetUserInfoAsync(password);
    }

    /// <summary>
    /// 修改当前用户信息
    /// </summary>
    /// <returns></returns>
    [HttpPut, ApiDescriptionSettings(Name = "userinfo")]
    public async Task PutUserInfoAsync([FromBody] UserInfo input)
    {
        await _authService.PutUserInfoAsync(input);
    }

    /// <summary>
    /// 获取当前用户组织机构树
    /// </summary>
    /// <returns></returns>
    [HttpGet, ApiDescriptionSettings(Name = "organization-tree")]
    public async Task<List<OrganizationProfile>> GetOrganizationTreeAsync()
    {
        return await _authService.GetOrganizationTreeAsync();
    }
}
