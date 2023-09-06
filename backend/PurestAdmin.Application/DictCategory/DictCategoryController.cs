// PurestAdmin
// Author:dym
// MIT License
// https://gitee.com/dymproject/purest

using PurestAdmin.Application.DictCategory.Dtos;
using PurestAdmin.Application.DictCategory.Services;
using PurestAdmin.Core.Consts;

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
