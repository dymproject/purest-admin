// MIT 许可证
// 版权 © 2023-present https://github.com/dymproject/purest-admin作者 以及贡献者
// 作者或版权持有人都不对任何索赔、损害或其他责任负责，无论这些追责来自合同、侵权或其它行为中，
// 还是产生于、源于或有关于本软件以及本软件的使用或其它处置。

using Newtonsoft.Json;

using PurestAdmin.Core.OopsExtension;
using PurestAdmin.Workflow.Models;

using WorkflowCore.Models;
using WorkflowCore.Models.DefinitionStorage.v1;
using WorkflowCore.Services.DefinitionStorage;

namespace PurestAdmin.Workflow;
public class WorkflowManager(IWorkflowRegistry registry, IDefinitionLoader definitionLoader, ILogger logger)
{
    private readonly IWorkflowRegistry _registry = registry;
    protected readonly IDefinitionLoader _definitionLoader = definitionLoader;
    private readonly ILogger _logger = logger;
    /// <summary>
    /// 注册json工作流实例
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public WorkflowDefinition RegisterInstance(WorkflowTemplateModel input)
    {
        if (_registry.IsRegistered(input.TemplateId, input.Version))
        {
            throw Oops.Bah($"the workflow {input.TemplateId} has ben definded!");
        }
        DefinitionSourceV1 source = new()
        {
            Id = input.TemplateId,
            Version = input.Version,
            Description = input.Description,
            Steps = [],
            DataType = $"{typeof(Dictionary<string, object>).FullName}, {typeof(Dictionary<string, object>).Assembly.FullName}"
        };

        StepSourceV1 v1 = new StepSourceV1();
        //这里实现具体的step

        var json = JsonConvert.SerializeObject(source);
        var def = _definitionLoader.LoadDefinition(json, Deserializers.Json);
        return def;
    }
}
