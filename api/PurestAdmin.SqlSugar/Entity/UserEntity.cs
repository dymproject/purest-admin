// MIT 许可证
// 版权 © 2023-present https://github.com/dymproject/purest-admin作者 以及贡献者
// 作者或版权持有人都不对任何索赔、损害或其他责任负责，无论这些追责来自合同、侵权或其它行为中，
// 还是产生于、源于或有关于本软件以及本软件的使用或其它处置。

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
    /// 组织机构Id
    /// </summary>
    [SugarColumn(ColumnName = "ORGANIZATION_ID")]
    public long OrganizationId { get; set; }
}