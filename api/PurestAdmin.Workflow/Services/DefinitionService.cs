// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

using Newtonsoft.Json;

using PurestAdmin.Workflow.Services.InstanceDtos;
using PurestAdmin.Workflow.Services.WfDefiniationDtos;

using WorkflowCore.Models.DefinitionStorage.v1;
using WorkflowCore.Services.DefinitionStorage;

namespace PurestAdmin.Workflow.Services;
/// <summary>
/// WfDefinition服务
/// </summary>
[ApiExplorerSettings(GroupName = ApiExplorerGroupConst.WORKFLOW)]
public class DefinitionService(ISqlSugarClient db, IDefinitionLoader definitionLoader) : ApplicationService
{
    private readonly ISqlSugarClient _db = db;
    private readonly IDefinitionLoader _definitionLoader = definitionLoader;
    /// <summary>
    /// 分页查询
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task<PagedList<WfDefinitionOutput>> GetPagedListAsync(GetPagedListInput input)
    {
        var pagedList = await _db.Queryable<WfDefinitionEntity>()
            .WhereIF(!input.Name.IsNullOrEmpty(), x => x.Name.Contains(input.Name))
            .WhereIF(input.Version.HasValue, x => x.Version == input.Version)
            .WhereIF(input.IsLocked.HasValue, x => x.IsLocked == input.IsLocked)
            .ToPurestPagedListAsync(input.PageIndex, input.PageSize);
        return pagedList.Adapt<PagedList<WfDefinitionOutput>>();
    }

    /// <summary>
    /// 单条查询
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<WfDefinitionOutput> GetAsync(long id)
    {
        var entity = await _db.Queryable<WfDefinitionEntity>().FirstAsync(x => x.Id == id);
        return entity.Adapt<WfDefinitionOutput>();
    }

    /// <summary>
    /// 添加
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task<long> AddAsync(AddWfDefinitionInput input)
    {
        var entity = input.Adapt<WfDefinitionEntity>();
        entity.DefinitionId = Guid.NewGuid().ToString();
        entity.WorkflowContent = GetWorkflowContent(entity);
        return await _db.Insertable(entity).ExecuteReturnSnowflakeIdAsync();
    }

    /// <summary>
    /// 编辑
    /// </summary>
    /// <param name="id"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task PutAsync(long id, PutWfDefinitionInput input)
    {
        var entity = await _db.Queryable<WfDefinitionEntity>().FirstAsync(x => x.Id == id) ?? throw PersistdValidateException.Message(ErrorTipsEnum.NoResult);
        if (entity.IsLocked)
        {
            throw BusinessValidateException.Message("已锁定的流程无法编辑!");
        }
        var newEntity = input.Adapt(entity);
        newEntity.WorkflowContent = GetWorkflowContent(newEntity);
        _ = await _db.Updateable(newEntity).ExecuteCommandAsync();
    }

    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task DeleteAsync(long id)
    {
        var entity = await _db.Queryable<WfDefinitionEntity>().FirstAsync(x => x.Id == id) ?? throw PersistdValidateException.Message(ErrorTipsEnum.NoResult);
        _ = await _db.Deleteable(entity).ExecuteCommandAsync();
    }

    /// <summary>
    /// 锁定
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task LockAsync(long id)
    {
        var entity = await _db.Queryable<WfDefinitionEntity>().FirstAsync(x => x.Id == id) ?? throw PersistdValidateException.Message(ErrorTipsEnum.NoResult);
        entity.IsLocked = true;
        _definitionLoader.LoadDefinition(entity.WorkflowContent, Deserializers.Json);
        _ = await _db.Updateable(entity).ExecuteCommandAsync();
    }

    /// <summary>
    /// 已注册流程集合
    /// </summary>
    /// <returns></returns>
    public async Task<List<WfDefinitionOutput>> GetDefinitionsAsync()
    {
        var list = await _db.Queryable<WfDefinitionEntity>().Where(x => x.IsLocked).ToListAsync();
        return list.Adapt<List<WfDefinitionOutput>>();
    }

    #region 私有方法
    /// <summary>
    /// 转换
    /// </summary>
    /// <param name="entity"></param>
    private static string GetWorkflowContent(WfDefinitionEntity entity)
    {
        var v1 = new DefinitionSourceV1()
        {
            Id = entity.DefinitionId,
            DataType = "PurestAdmin.Workflow.DataTypes.GeneralAuditingDefinition, PurestAdmin.Workflow",
            Version = entity.Version,
            Description = entity.Name,
            Steps = []
        };
        var logicFlowDto = JsonConvert.DeserializeObject<LogicFlowDto>(entity.DesignsContent) ?? new LogicFlowDto();
        var startNode = logicFlowDto.Nodes.First(x => x.Type == "Start");
        var endNode = logicFlowDto.Nodes.First(x => x.Type == "End");
        var newNodes = logicFlowDto.Nodes.Where(x => !"Start,End".Split(',').Contains(x.Type)).ToList();
        newNodes.AddFirst(startNode);
        newNodes.AddLast(endNode);
        foreach (var item in newNodes)
        {
            var step = new StepSourceV1()
            {
                Id = item.Id,
                Name = item.Text?.Value ?? "",
                StepType = item.GetWorkflowStepType()

            };
            foreach (var p in item.Properties)
            {
                step.Inputs.TryAdd(string.Concat(p.Key.First().ToString().ToUpper(), p.Key.AsSpan(1)), p.Value is string ? "\"" + p.Value.ToString() + "\"" : p.Value.ToString());
            }
            var edges = logicFlowDto.Edges.Where(x => x.SourceNodeId == item.Id).ToList();
            if (item.Type == "GeneralAuditing")
            {
                switch (edges.Count)
                {
                    case 0:
                        throw BusinessValidateException.Message("有节点未连接");
                    case 1:
                        step.SelectNextStep.Add(edges.First().TargetNodeId, $"int.Parse(data[\"GeneralAuditingDataResultEnum\"].ToString()) == {(int)GeneralAuditingDataResultEnum.Proceed}");
                        break;
                    case > 1:
                        foreach (var edge in edges)
                        {
                            if (decimal.TryParse(edge.Properties?.Value, out var value))
                            {
                                step.SelectNextStep.Add(edge.TargetNodeId, $"decimal.Parse(data[\"{edge.Properties.Field}\"].ToString()) {edge.Properties.Operate} {value} " +
                                    $"&& int.Parse(data[\"GeneralAuditingDataResultEnum\"].ToString()) == {(int)GeneralAuditingDataResultEnum.Proceed}");
                            }
                            else
                            {
                                throw BusinessValidateException.Message("条件转换失败,只支持数字相关判断");
                            }
                        }
                        break;
                }
                var exist = step.SelectNextStep.Any(x => x.Key == endNode.Id);
                if (exist)
                {
                    step.SelectNextStep[endNode.Id] += $" || int.Parse(data[\"GeneralAuditingDataResultEnum\"].ToString()) == {(int)GeneralAuditingDataResultEnum.Discontinue}";
                }
                else
                {
                    step.SelectNextStep.Add(endNode.Id, $"int.Parse(data[\"GeneralAuditingDataResultEnum\"].ToString()) == {(int)GeneralAuditingDataResultEnum.Discontinue}");
                }
            }
            else
            {
                step.NextStepId = edges.FirstOrDefault() == null ? null : edges.First().TargetNodeId;
            }
            v1.Steps.Add(step);
        }
        return JsonConvert.SerializeObject(v1);
    }

    #endregion
}
