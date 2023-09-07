// MIT 许可证
// 版权 © 2023-present dym 和所有贡献者
// 作者或版权持有人都不对任何索赔、损害或其他责任负责，无论这些追责来自合同、侵权或其它行为中，
// 还是产生于、源于或有关于本软件以及本软件的使用或其它处置。

using PurestAdmin.Application.User.Dtos;

namespace PurestAdmin.Application.User.Services;
/// <summary>
/// 用户接口
/// </summary>
public interface IUserService
{
    /// <summary>
    /// 分页查询
    /// </summary>
    Task<PagedList<UserProfile>> GetPagedListAsync(GetPagedListInput input);
    /// <summary>
    /// 单条查询
    /// </summary>
    Task<UserProfile> GetAsync(long id);
    /// <summary>
    /// 添加数据
    /// </summary>
    Task<long> AddAsync(AddUserInput input);
    /// <summary>
    /// 编辑数据
    /// </summary>
    Task PutAsync(long id, EditUserInput input);
    /// <summary>
    /// 删除数据
    /// </summary>
    Task DeleteAsync(long id);
    /// <summary>
    /// 重置密码
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<string> ResetPassword(long id);
}