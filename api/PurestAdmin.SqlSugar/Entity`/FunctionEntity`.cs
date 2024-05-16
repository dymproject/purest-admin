// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

namespace PurestAdmin.SqlSugar.Entity;

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