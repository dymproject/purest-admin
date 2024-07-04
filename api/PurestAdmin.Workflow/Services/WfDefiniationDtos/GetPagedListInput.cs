// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

namespace PurestAdmin.Workflow.Services.WfDefiniationDtos;
public class GetPagedListInput : PaginationParams
{
    /// <summary>
    /// 模板名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 版本
    /// </summary>
    public int? Version { get; set; }

    /// <summary>
    /// 是否锁定
    /// </summary>
    public bool? IsLocked { get; set; }
}
