// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

namespace PurestAdmin.Workflow.DefinitionModels;
public class StepModel
{
    public string Id { get; set; }
    public string NextStepId { get; set; }
    public string StepType { get; set; }
    public Dictionary<string, object> Inputs { get; set; }
    public Dictionary<string, object> Outputs { get; set; }
}
