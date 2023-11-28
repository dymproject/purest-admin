// MIT 许可证
// 版权 © 2023-present dym 和所有贡献者
// 作者或版权持有人都不对任何索赔、损害或其他责任负责，无论这些追责来自合同、侵权或其它行为中，
// 还是产生于、源于或有关于本软件以及本软件的使用或其它处置。

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
    /// 获取当前用户信息
    /// </summary>
    /// <returns></returns>
    Task<UserInfo> GetUserInfoAsync(string password);
    /// <summary>
    /// 修改当前用户信息
    /// </summary>
    /// <returns></returns>
    Task PutUserInfoAsync(UserInfo input);
    /// <summary>
    /// 获取当前用户组织机构树
    /// </summary>
    /// <returns></returns>
    Task<List<OrganizationProfile>> GetOrganizationTreeAsync();
}
