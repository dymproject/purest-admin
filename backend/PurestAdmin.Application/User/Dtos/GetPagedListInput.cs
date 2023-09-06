// PurestAdmin
// Author:dym
// MIT License
// https://gitee.com/dymproject/purest

namespace PurestAdmin.Application.User.Dtos;

/// <summary>
/// 用户查询
/// </summary>
public class GetPagedListInput : PaginationParams
{
    /// <summary>
    /// 账号
    /// </summary>
    public string Account { get; set; }
    /// <summary>
    /// 用户名
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// 电话
    /// </summary>
    public string Telephone { get; set; }
    /// <summary>
    /// 邮箱
    /// </summary>
    public string Email { get; set; }
}