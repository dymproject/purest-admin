// MIT 许可证
// 版权 © 2023-present dym 和所有贡献者
// 作者或版权持有人都不对任何索赔、损害或其他责任负责，无论这些追责来自合同、侵权或其它行为中，
// 还是产生于、源于或有关于本软件以及本软件的使用或其它处置。

using PurestAdmin.Application.Function.Dtos;

namespace PurestAdmin.Application.Function.Services;
/// <summary>
/// 功能接口实现
/// </summary>
public class FunctionService : IFunctionService, ITransient
{
    private readonly ISqlSugarClient _db;
    private readonly Repository<FunctionEntity> _functionRepository;
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="db"></param>
    /// <param name="functionRepository"></param>
    public FunctionService(ISqlSugarClient db, Repository<FunctionEntity> functionRepository)
    {
        _db = db;
        _functionRepository = functionRepository;
    }

    /// <summary>
    /// 分页查询
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task<PagedList<FunctionProfile>> GetPagedListAsync(GetPagedListInput input)
    {
        var tree = input.Name.IsNullOrEmpty()
            ? await _functionRepository.AsQueryable().ToTreeAsync(x => x.Children, x => x.ParentId, null)
            : await _functionRepository.AsQueryable().Where(x => x.Name.Contains(input.Name)).ToListAsync();
        return tree.Adapt<List<FunctionProfile>>().ToFinalPagedList(input.PageIndex, input.PageSize);
    }

    /// <summary>
    /// 单条查询
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<FunctionProfile> GetAsync(long id)
    {
        var entity = await _functionRepository.GetByIdAsync(id);
        return entity.Adapt<FunctionProfile>();
    }

    /// <summary>
    /// 添加
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task<long> AddAsync(AddFunctionInput input)
    {
        var entity = input.Adapt<FunctionEntity>();
        return await _functionRepository.InsertReturnSnowflakeIdAsync(entity);
    }

    /// <summary>
    /// 编辑
    /// </summary>
    /// <param name="id"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task PutAsync(long id, AddFunctionInput input)
    {
        var entity = await _functionRepository.GetByIdAsync(id) ?? throw Oops.Bah(ErrorTipsEnum.NoResult);
        var newEntity = input.Adapt(entity);
        _ = await _functionRepository.UpdateAsync(newEntity);
    }

    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task DeleteAsync(long id)
    {
        var entity = await _functionRepository.GetByIdAsync(id) ?? throw Oops.Bah(ErrorTipsEnum.NoResult);
        await _functionRepository.DeleteAsync(entity);
    }

    /// <summary>
    /// 树查询
    /// </summary>
    public async Task<List<FunctionProfile>> GetFunctionTreeAsync()
    {
        var tree = await _db.Queryable<FunctionEntity>().ToTreeAsync(x => x.Children, x => x.ParentId, null);
        return tree.Adapt<List<FunctionProfile>>();
    }

    /// <summary>
    /// 获取功能拥有的接口
    /// </summary>
    /// <param name="functionId"></param>
    /// <returns></returns>
    public async Task<List<BindedInterfaceProfile>> GetInterfacesAsync(long functionId)
    {
        var interfaces = await _db.Queryable<FunctionInterfaceEntity>()
            .LeftJoin<InterfaceEntity>((f, i) => f.InterfaceId == i.Id)
            .Where((f, i) => f.FunctionId == functionId)
            .Select((f, i) => new BindedInterfaceProfile
            {
                Id = f.Id,
                Name = i.Name,
                Path = i.Path,
                InterfaceId = f.InterfaceId
            })
            .ToListAsync();
        return interfaces;
    }

    /// <summary>
    /// 给功能分配接口
    /// </summary>
    /// <param name="functionId"></param>
    /// <param name="interfaceId"></param>
    public async Task AssignInterfaceAsync(long functionId, long interfaceId)
    {
        var record = await _db.Queryable<FunctionInterfaceEntity>().Where(x => x.FunctionId == functionId && x.InterfaceId == interfaceId).FirstAsync();
        if (record == null)
        {
            _ = await _db.Insertable(new FunctionInterfaceEntity { FunctionId = functionId, InterfaceId = interfaceId }).ExecuteReturnSnowflakeIdAsync();
        }
        else
        {
            _ = await _db.Deleteable<FunctionInterfaceEntity>(record).ExecuteCommandAsync();
        }
    }

    /// <summary>
    /// 移除功能的接口
    /// </summary>
    /// <param name="id"></param>
    public async Task RemoveInterfaceAsync(long id)
    {
        var record = await _db.Queryable<FunctionInterfaceEntity>().FirstAsync(x => x.Id == id) ?? throw Oops.Bah(ErrorTipsEnum.NoResult);
        await _db.Deleteable(record).ExecuteCommandAsync();
    }

}
