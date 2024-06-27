// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

namespace PurestAdmin.Workflow.Services.WfDefiniationDtos;
public class WfDefinitionOutput
{
    /// <summary>
    /// 主键Id
    /// </summary>
    public long Id { get; set; }
    /// <summary>
    /// 备注
    /// </summary>
    public string Remark { get; set; }
    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// 定义ID
    /// </summary>
    public string DefinitionId { get; set; }
    /// <summary>
    /// 流程内容
    /// </summary>
    public string WorkflowContent { get; set; }
    /// <summary>
    /// 设计器内容
    /// </summary>
    public string DesignsContent { get; set; }
    /// <summary>
    /// 表单内容
    /// </summary>
    public string FormContent { get; set; }
    /// <summary>
    /// 版本
    /// </summary>
    public int Version { get; set; }
    /// <summary>
    /// 是否锁定
    /// </summary>
    public bool IsLocked { get; set; }
}
