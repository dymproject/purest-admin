// PurestAdmin
// Author:dym
// MIT License
// https://gitee.com/dymproject/purest

using PurestAdmin.Application.DictCategory.Dtos;

namespace PurestAdmin.Application.DictCategory.Services;
/// <summary>
/// 字典分类接口
/// </summary>
public interface IDictCategoryService
{
    /// <summary>
    /// 分页查询
    /// </summary>
    Task<PagedList<DictCategoryProfile>> GetPagedListAsync(GetPagedListInput input);
    /// <summary>
    /// 单条查询
    /// </summary>
    Task<DictCategoryProfile> GetAsync(long id);
    /// <summary>
    /// 添加数据
    /// </summary>
    Task<long> AddAsync(AddDictCategoryInput input);
    /// <summary>
    /// 编辑数据
    /// </summary>
    Task PutAsync(long id, AddDictCategoryInput input);
    /// <summary>
    /// 删除数据
    /// </summary>
    Task DeleteAsync(long id);
}