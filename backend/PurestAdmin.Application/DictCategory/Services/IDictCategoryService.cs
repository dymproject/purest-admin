// MIT 许可证
// 版权 © 2023-present dym 和所有贡献者
// 作者或版权持有人都不对任何索赔、损害或其他责任负责，无论这些追责来自合同、侵权或其它行为中，
// 还是产生于、源于或有关于本软件以及本软件的使用或其它处置。

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