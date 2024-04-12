// MIT 许可证
// 版权 © 2023-present https://github.com/dymproject/purest-admin作者 以及贡献者
// 作者或版权持有人都不对任何索赔、损害或其他责任负责，无论这些追责来自合同、侵权或其它行为中，
// 还是产生于、源于或有关于本软件以及本软件的使用或其它处置。

using PurestAdmin.Application.DictCategoryServices.Dtos;


namespace PurestAdmin.Application.DictCategoryServices;
/// <summary>
/// 字典分类服务
/// </summary>
public class DictCategoryService(ISqlSugarClient db, Repository<DictCategoryEntity> dictCategoryRepository) : ApplicationService
{
    private readonly ISqlSugarClient _db = db;
    private readonly Repository<DictCategoryEntity> _dictCategoryRepository = dictCategoryRepository;

    /// <summary>
    /// 分页查询
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task<PagedList<DictCategoryOutput>> GetPagedListAsync(GetPagedListInput input)
    {
        var pagedList = await _db.Queryable<DictCategoryEntity>()
            .WhereIF(!input.Name.IsNullOrEmpty(), x => x.Name.Contains(input.Name.Trim()))
            .OrderByDescending(x => x.CreateTime)
            .ToPurestPagedListAsync(input.PageIndex, input.PageSize);
        return pagedList.Adapt<PagedList<DictCategoryOutput>>();
    }

    /// <summary>
    /// 单条查询
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<DictCategoryOutput> GetAsync(long id)
    {
        var entity = await _dictCategoryRepository.GetByIdAsync(id);
        return entity.Adapt<DictCategoryOutput>();
    }

    /// <summary>
    /// 添加
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task<long> AddAsync(AddDictCategoryInput input)
    {
        var entity = input.Adapt<DictCategoryEntity>();
        return await _dictCategoryRepository.InsertReturnSnowflakeIdAsync(entity);
    }

    /// <summary>
    /// 编辑
    /// </summary>
    /// <param name="id"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task PutAsync(long id, AddDictCategoryInput input)
    {
        var entity = await _dictCategoryRepository.GetByIdAsync(id) ?? throw Oops.Bah(ErrorTipsEnum.NoResult);
        var newEntity = input.Adapt(entity);
        _ = await _dictCategoryRepository.UpdateAsync(newEntity);
    }

    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task DeleteAsync(long id)
    {
        var entity = await _dictCategoryRepository.GetByIdAsync(id) ?? throw Oops.Bah(ErrorTipsEnum.NoResult);
        await _dictCategoryRepository.DeleteAsync(entity);
    }

}
