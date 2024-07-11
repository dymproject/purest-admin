// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

namespace PurestAdmin.Workflow.Services.InstanceDtos;
public class AuditingDetailOutput
{
    /// <summary>
    /// 表单内容
    /// </summary>
    public string FormContent { get; set; }
    /// <summary>
    /// 流程数据
    /// </summary>
    public object FormData { get; set; }
}
