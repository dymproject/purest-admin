// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

using System.ComponentModel;

namespace PurestAdmin.Workflow.Services.WfDefiniationDtos;
public class AddWfDefinitionInput
{
    /// <summary>
    /// 备注
    /// </summary>
    public string Remark { get; set; }
    /// <summary>
    /// 名称
    /// </summary>
    [Required(ErrorMessage = "名称不能为空"), MaxLength(20, ErrorMessage = "名称最大长度为：20")]
    public string Name { get; set; }
    /// <summary>
    /// 流程内容
    /// </summary>
    [Required(ErrorMessage = "流程内容不能为空")]
    public string WorkflowContent { get; set; }
    /// <summary>
    /// 设计器内容
    /// </summary>
    [Required(ErrorMessage = "设计器内容不能为空")]
    public string DesignsContent { get; set; }
    /// <summary>
    /// 表单内容
    /// </summary>
    [Required(ErrorMessage = "表单内容不能为空")]
    public string FormContent { get; set; }
    /// <summary>
    /// 版本
    /// </summary>
    [Required(ErrorMessage = "版本不能为空"), DefaultValue(1)]
    public int Version { get; set; }
    /// <summary>
    /// 是否锁定
    /// </summary>
    [Required(ErrorMessage = "是否锁定不能为空")]
    public bool IsLocked { get; set; }
}
