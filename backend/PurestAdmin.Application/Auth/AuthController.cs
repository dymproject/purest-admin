// PurestAdmin
// Author:dym
// MIT License
// https://gitee.com/dymproject/purest

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
    public async Task<UserInfoOutput> GetUserInfoAsync()
    {
        return await _authService.GetUserInfoAsync();
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
