// PurestAdmin
// Author:dym
// MIT License
// https://gitee.com/dymproject/purest

namespace PurestAdmin.Core.Entity;
public partial class UserEntity
{
    /// <summary>
    /// 角色
    /// </summary>
    [SugarColumn(IsIgnore = true)]
    public RoleEntity Role { get; set; }
}
