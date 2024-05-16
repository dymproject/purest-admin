// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

namespace PurestAdmin.SqlSugar.Entity;

/// <summary>
/// 用户
/// </summary>
[SugarTable("PUREST_USER")]
public partial class UserEntity : BaseEntity
{
    /// <summary>
    /// 账号
    /// </summary>
    [SugarColumn(ColumnName = "ACCOUNT")]
    public string Account { get; set; }
    /// <summary>
    /// 密码
    /// </summary>
    [SugarColumn(ColumnName = "PASSWORD")]
    public string Password { get; set; }
    /// <summary>
    /// 真实姓名
    /// </summary>
    [SugarColumn(ColumnName = "NAME")]
    public string Name { get; set; }
    /// <summary>
    /// 电话
    /// </summary>
    [SugarColumn(ColumnName = "TELEPHONE")]
    public string Telephone { get; set; }
    /// <summary>
    /// 邮箱
    /// </summary>
    [SugarColumn(ColumnName = "EMAIL")]
    public string Email { get; set; }
    /// <summary>
    /// 头像
    /// </summary>
    [SugarColumn(ColumnName = "AVATAR")]
    public byte[] Avatar { get; set; }
    /// <summary>
    /// 状态
    /// </summary>
    [SugarColumn(ColumnName = "STATUS")]
    public int Status { get; set; }
    /// <summary>
    /// 组织机构Id
    /// </summary>
    [SugarColumn(ColumnName = "ORGANIZATION_ID")]
    public long OrganizationId { get; set; }
}