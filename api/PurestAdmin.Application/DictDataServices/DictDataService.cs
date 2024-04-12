// MIT 许可证
// 版权 © 2023-present https://github.com/dymproject/purest-admin作者 以及贡献者
// 作者或版权持有人都不对任何索赔、损害或其他责任负责，无论这些追责来自合同、侵权或其它行为中，
// 还是产生于、源于或有关于本软件以及本软件的使用或其它处置。

using PurestAdmin.Application.DictDataServices.Dtos;


namespace PurestAdmin.Application.DictDataServices;
/// <summary>
/// 字典数据服务
/// </summary>
public class DictDataService(ISqlSugarClient db, Repository<DictDataEntity> dictDataRepository) : ApplicationService
{
    private readonly ISqlSugarClient _db = db;
    private readonly Repository<DictDataEntity> _dictDataRepository = dictDataRepository;

    /// <summary>
    /// 分页查询
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task<PagedList<DictDataOutput>> GetPagedListAsync(GetPagedListInput input)
    {
        var pagedList = await _db.Queryable<DictDataEntity>()
            .Where(x => x.CategoryId == input.CategoryId)
            .WhereIF(!input.Name.IsNullOrEmpty(), x => x.Name.Contains(input.Name.Trim()))
            .OrderByDescending(x => x.Sort)
            .OrderByDescending(x => x.CreateTime)
            .ToPurestPagedListAsync(input.PageIndex, input.PageSize);
        return pagedList.Adapt<PagedList<DictDataOutput>>();
    }

    /// <summary>
    /// 查询分类下的所有数据
    /// </summary>
    /// <param name="categoryCode"></param>
    /// <returns></returns>
    public async Task<List<DictDataOutput>> GetListAsync(string categoryCode)
    {
        var list = await _dictDataRepository.AsQueryable()
            .LeftJoin<DictCategoryEntity>((d, c) => d.CategoryId == c.Id)
            .Where((d, c) => c.Code.Equals(categoryCode, StringComparison.CurrentCultureIgnoreCase))
            .Select((d) => d)
            .ToListAsync();
        return list.Adapt<List<DictDataOutput>>();
    }

    /// <summary>
    /// 单条查询
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<DictDataOutput> GetAsync(long id)
    {
        var entity = await _dictDataRepository.GetByIdAsync(id);
        return entity.Adapt<DictDataOutput>();
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
