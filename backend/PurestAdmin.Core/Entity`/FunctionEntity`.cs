// PurestAdmin
// Author:dym
// MIT License
// https://gitee.com/dymproject/purest

namespace PurestAdmin.Core.Entity;

/// <summary>
/// 功能实体扩展
/// </summary>
public partial class FunctionEntity
{
    /// <summary>
    /// 子集
    /// </summary>
    [SugarColumn(IsIgnore = true)]
    public List<FunctionEntity> Children { get; set; }
}