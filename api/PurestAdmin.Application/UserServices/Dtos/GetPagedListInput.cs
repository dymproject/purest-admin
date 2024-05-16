// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

namespace PurestAdmin.Application.UserServices.Dtos;

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
    /// <summary>
    /// 账户状态
    /// </summary>
    public int? Status { get; set; }
}