// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

using PurestAdmin.Workflow.Services.WfDefiniationDtos;

namespace PurestAdmin.Workflow.Services;
/// <summary>
/// WfDefinition服务
/// </summary>
[ApiExplorerSettings(GroupName = ApiExplorerGroupConst.WORKFLOW)]
public class DefinitionService(ISqlSugarClient db) : ApplicationService
{
    private readonly ISqlSugarClient _db = db;

    /// <summary>
    /// 分页查询
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task<PagedList<WfDefinitionOutput>> GetPagedListAsync(GetPagedListInput input)
    {
        var pagedList = await _db.Queryable<WfDefinitionEntity>()
            .Where(x => x.IsLocked)
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
        var newEntity = input.Adapt(entity);
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
        _ = await _db.Updateable(entity).ExecuteCommandAsync();
    }

    /// <summary>
    /// 解锁
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task UnlockAsync(long id)
    {
        var entity = await _db.Queryable<WfDefinitionEntity>().FirstAsync(x => x.Id == id) ?? throw PersistdValidateException.Message(ErrorTipsEnum.NoResult);
        entity.IsLocked = false;
        _ = await _db.Updateable(entity).ExecuteCommandAsync();
    }
}
