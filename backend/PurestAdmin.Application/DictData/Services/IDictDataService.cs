// PurestAdmin
// Author:dym
// MIT License
// https://gitee.com/dymproject/purest

using PurestAdmin.Application.DictData.Dtos;

namespace PurestAdmin.Application.DictData.Services;
/// <summary>
/// 字典数据接口
/// </summary>
public interface IDictDataService
{
    /// <summary>
    /// 分页查询
    /// </summary>
    Task<PagedList<DictDataProfile>> GetPagedListAsync(GetPagedListInput input);
    /// <summary>
    /// 查询分类下的所有数据
    /// </summary>
    /// <param name="categoryCode"></param>
    /// <returns></returns>
    Task<List<DictDataProfile>> GetListAsync(string categoryCode);
    /// <summary>
    /// 单条查询
    /// </summary>
    Task<DictDataProfile> GetAsync(long id);
    /// <summary>
    /// 添加数据
    /// </summary>
    Task<long> AddAsync(AddDictDataInput input);
    /// <summary>
    /// 编辑数据
    /// </summary>
    Task PutAsync(long id, AddDictDataInput input);
    /// <summary>
    /// 删除数据
    /// </summary>
    Task DeleteAsync(long id);
}