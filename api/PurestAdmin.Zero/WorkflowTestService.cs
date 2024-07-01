// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

using Volo.Abp.DependencyInjection;

using WorkflowCore.Interface;

namespace PurestAdmin.Zero;
public class WorkflowTestService(IWorkflowHost workflowHost) : ISingletonDependency
{
    private readonly IWorkflowHost _workflowhost = workflowHost;
    public async void Initialization()
    {
        #region 01
        //_workflowhost.StartWorkflow("HelloWorld");
        #endregion
        #region 04 
        //var initialData = new MyDataClass();
        //var workflowId = _workflowhost.StartWorkflow("EventSampleWorkflow", 1, initialData).Result;

        //Console.WriteLine("Enter value to publish");
        //string value = Console.ReadLine();
        //_workflowhost.PublishEvent("MyEvent", workflowId, value);
        #endregion
        #region 18
        //var workflowId = _workflowhost.StartWorkflow("activity-sample", new MyData { Request = "Spend $1,000,000" }).Result;

        //var approval = _workflowhost.GetPendingActivity("get-approval", "worker1", TimeSpan.FromMinutes(1)).Result;

        //if (approval != null)
        //{
        //    Console.WriteLine("Approval required for " + approval.Parameters);
        //    _workflowhost.SubmitActivitySuccess(approval.Token, "John Smith");
        //}
        #endregion

        #region 11
        //Console.WriteLine("Starting workflow...");
        //string workflowId = _workflowhost.StartWorkflow("if-sample", new MyData() { }).Result;
        //Console.WriteLine("请输入数字");
        //var count = Console.ReadLine() ?? "0";
        //await _workflowhost.PublishEvent("BranchEvent", workflowId, int.Parse(count));
        //Console.ReadLine();
        #endregion
    }
}
