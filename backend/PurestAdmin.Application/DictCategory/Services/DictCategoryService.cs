// PurestAdmin
// Author:dym
// MIT License
// https://gitee.com/dymproject/purest

using PurestAdmin.Application.DictCategory.Dtos;

namespace PurestAdmin.Application.DictCategory.Services;
/// <summary>
/// 字典分类服务
/// </summary>
public class DictCategoryService : IDictCategoryService, ITransient
{
    private readonly ISqlSugarClient _db;
    private readonly Repository<DictCategoryEntity> _dictCategoryRepository;
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="db"></param>
    /// <param name="dictCategoryRepository"></param>
    public DictCategoryService(ISqlSugarClient db, Repository<DictCategoryEntity> dictCategoryRepository)
    {
        _db = db;
        _dictCategoryRepository = dictCategoryRepository;
    }

    /// <summary>
    /// 分页查询
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task<PagedList<DictCategoryProfile>> GetPagedListAsync(GetPagedListInput input)
    {
        var pagedList = await _db.Queryable<DictCategoryEntity>()
            .WhereIF(!input.Name.IsNullOrEmpty(), x => x.Name.Contains(input.Name.Trim()))
            .OrderByDescending(x => x.CreateTime)
            .ToFinalPagedListAsync(input.PageIndex, input.PageSize);
        return pagedList.Adapt<PagedList<DictCategoryProfile>>();
    }

    /// <summary>
    /// 单条查询
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<DictCategoryProfile> GetAsync(long id)
    {
        var entity = await _dictCategoryRepository.GetByIdAsync(id);
        return entity.Adapt<DictCategoryProfile>();
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