// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

namespace PurestAdmin.Multiplex.Contracts.BackgroundArgs;
public class CountersignAuditingArgs
{
    /// <summary>
    /// 工作流Id
    /// </summary>
    public string InstanceId { get; set; }
    /// <summary>
    /// 步骤Id
    /// </summary>
    public string ExecutionPointerId { get; set; }
}
