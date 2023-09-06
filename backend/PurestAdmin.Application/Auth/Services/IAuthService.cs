// PurestAdmin
// Author:dym
// MIT License
// https://gitee.com/dymproject/purest

using PurestAdmin.Application.Auth.Dtos;
using PurestAdmin.Application.Organization.Dtos;

namespace PurestAdmin.Application.Auth.Services;
/// <summary>
/// 用户授权服务接口
/// </summary>
public interface IAuthService
{
    /// <summary>
    /// 用户登录
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<LoginOutput> LoginAsync([Required] LoginInput input);
    /// <summary>
    /// 获取当前用户权限信息
    /// </summary>
    /// <returns></returns>
    Task<UserInfoOutput> GetUserInfoAsync();
    /// <summary>
    /// 获取当前用户组织机构树
    /// </summary>
    /// <returns></returns>
    Task<List<OrganizationProfile>> GetOrganizationTreeAsync();
}
