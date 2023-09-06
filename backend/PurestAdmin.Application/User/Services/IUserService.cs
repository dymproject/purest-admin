// PurestAdmin
// Author:dym
// MIT License
// https://gitee.com/dymproject/purest

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