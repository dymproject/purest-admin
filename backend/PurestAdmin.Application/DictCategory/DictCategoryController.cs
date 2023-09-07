// MIT 许可证
// 版权 © 2023-present dym 和所有贡献者
// 作者或版权持有人都不对任何索赔、损害或其他责任负责，无论这些追责来自合同、侵权或其它行为中，
// 还是产生于、源于或有关于本软件以及本软件的使用或其它处置。

using PurestAdmin.Application.DictCategory.Dtos;
using PurestAdmin.Application.DictCategory.Services;

namespace PurestAdmin.Facade.Controllers;
/// <summary>
/// 字典分类接口
/// </summary>
[ApiDescriptionSettings(ApiGroupConst.SYSTEM)]
public class DictCategoryController : IDynamicApiController
{
    private readonly IDictCategoryService _dictCategoryService;
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="dictCategoryService"></param>
    public DictCategoryController(IDictCategoryService dictCategoryService)
    {
        _dictCategoryService = dictCategoryService;
    }

    /// <summary>
    /// 字典分类分页查询
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<PagedList<DictCategoryProfile>> GetCategoryPagedListAsync([FromQuery, Required] GetPagedListInput input)
    {
        return await _dictCategoryService.GetPagedListAsync(input);
    }

    /// <summary>
    /// 字典分类单条查询
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<DictCategoryProfile> GetAsync([ApiSeat(ApiSeats.ActionStart)] long id)
    {
        return await _dictCategoryService.GetAsync(id);
    }

    /// <summary>
    /// 字典分类添加
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost, UnitOfWork]
    public async Task<long> AddAsync(AddDictCategoryInput input)
    {
        return await _dictCategoryService.AddAsync(input);
    }

    /// <summary>
    /// 字典分类编辑
    /// </summary>
    /// <param name="id"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPut, UnitOfWork]
    public async Task PutAsync([ApiSeat(ApiSeats.ActionStart)] long id, [FromBody] AddDictCategoryInput input)
    {
        await _dictCategoryService.PutAsync(id, input);
    }

    /// <summary>
    /// 字典分类删除
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete]
    public async Task DeleteAsync([ApiSeat(ApiSeats.ActionStart)] long id)
    {
        await _dictCategoryService.DeleteAsync(id);
    }
}
