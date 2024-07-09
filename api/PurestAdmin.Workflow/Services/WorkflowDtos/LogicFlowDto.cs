// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

namespace PurestAdmin.Workflow.Services.WorkflowDtos;
public class LogicFlowDto
{
    public LogicNode[] Nodes { get; set; } = [];

    public LogicEdge[] Edges { get; set; } = [];
}

public class LogicNode
{
    public string Id { get; set; }

    public string Type { get; set; }

    public Dictionary<string, object> Properties { get; set; }

    public LogicText Text { get; set; }

    public string GetWorkflowStepType()
    {
        return Type switch
        {
            "Start" => "PurestAdmin.Workflow.Steps.StartSetp,PurestAdmin.Workflow",
            "End" => "PurestAdmin.Workflow.Steps.OverStep,PurestAdmin.Workflow",
            "GeneralAuditing" => "PurestAdmin.Workflow.Steps.GeneralAuditingStep,PurestAdmin.Workflow",
            _ => "",
        };
    }
}

public class LogicText
{
    public string Value { get; set; }
}

public class LogicEdge
{
    public string Id { get; set; }

    public string Type { get; set; }

    public string SourceNodeId { get; set; }

    public string TargetNodeId { get; set; }

    public ConditionDto Properties { get; set; }

    public LogicText Text { get; set; }
}

public class ConditionDto
{
    public string Field { get; set; }
    public string Value { get; set; }
    public string Operate { get; set; }
}