// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

using Volo.Abp.DependencyInjection;

using WorkflowCore.Interface;

namespace PurestAdmin.Zero;
public class WorkflowTestService(IWorkflowHost workflowHost) : ISingletonDependency
{
    private readonly IWorkflowHost _workflowhost = workflowHost;
    public void Initialization()
    {
        //_workflowhost.Start();
    }
}
