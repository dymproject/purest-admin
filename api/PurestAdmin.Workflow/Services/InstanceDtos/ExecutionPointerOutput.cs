// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

namespace PurestAdmin.Workflow.Services.InstanceDtos;
public class ExecutionPointerOutput
{
    public int StepId { get; set; }
    public string StepName { get; set; }
    public int Status { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public List<AuditingRecordOutput> AuditingRecords { get; set; }
}
