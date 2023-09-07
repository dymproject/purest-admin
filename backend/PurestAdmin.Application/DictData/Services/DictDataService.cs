// MIT 许可证
// 版权 © 2023-present dym 和所有贡献者
// 作者或版权持有人都不对任何索赔、损害或其他责任负责，无论这些追责来自合同、侵权或其它行为中，
// 还是产生于、源于或有关于本软件以及本软件的使用或其它处置。

using PurestAdmin.Application.DictData.Dtos;

namespace PurestAdmin.Application.DictData.Services;
/// <summary>
/// 字典数据服务
/// </summary>
public class DictDataService : IDictDataService, ITransient
{
    private readonly ISqlSugarClient _db;
    private readonly Repository<DictDataEntity> _dictDataRepository;
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="db"></param>
    /// <param name="dictDataRepository"></param>
    public DictDataService(ISqlSugarClient db, Repository<DictDataEntity> dictDataRepository)
    {
        _db = db;
        _dictDataRepository = dictDataRepository;
    }

    /// <summary>
    /// 分页查询
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task<PagedList<DictDataProfile>> GetPagedListAsync(GetPagedListInput input)
    {
        var pagedList = await _db.Queryable<DictDataEntity>()
            .Where(x => x.CategoryId == input.CategoryId)
            .WhereIF(!input.Name.IsNullOrEmpty(), x => x.Name.Contains(input.Name.Trim()))
            .OrderByDescending(x => x.Sort)
            .OrderByDescending(x => x.CreateTime)
            .ToFinalPagedListAsync(input.PageIndex, input.PageSize);
        return pagedList.Adapt<PagedList<DictDataProfile>>();
    }

    /// <summary>
    /// 查询分类下的所有数据
    /// </summary>
    /// <param name="categoryCode"></param>
    /// <returns></returns>
    public async Task<List<DictDataProfile>> GetListAsync(string categoryCode)
    {
        var list = await _dictDataRepository.AsQueryable()
            .LeftJoin<DictCategoryEntity>((d, c) => d.CategoryId == c.Id)
            .Where((d, c) => c.Code.ToLower() == categoryCode.ToLower())
            .Select((d) => d)
            .ToListAsync();
        return list.Adapt<List<DictDataProfile>>();
    }

    /// <summary>
    /// 单条查询
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<DictDataProfile> GetAsync(long id)
    {
        var entity = await _dictDataRepository.GetByIdAsync(id);
        return entity.Adapt<DictDataProfile>();
    }

    /// <summary>
    /// 添加
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task<long> AddAsync(AddDictDataInput input)
    {
        var entity = input.Adapt<DictDataEntity>();
        return await _dictDataRepository.InsertReturnSnowflakeIdAsync(entity);
    }

    /// <summary>
    /// 编辑
    /// </summary>
    /// <param name="id"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task PutAsync(long id, AddDictDataInput input)
    {
        var entity = await _dictDataRepository.GetByIdAsync(id) ?? throw Oops.Bah(ErrorTipsEnum.NoResult);
        entity = input.Adapt(entity);
        _ = await _dictDataRepository.UpdateAsync(entity);
    }

    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task DeleteAsync(long id)
    {
        var entity = await _dictDataRepository.GetByIdAsync(id) ?? throw Oops.Bah(ErrorTipsEnum.NoResult);
        await _dictDataRepository.DeleteAsync(entity);
    }

}
