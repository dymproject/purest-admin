// PurestAdmin
// Author:dym
// MIT License
// https://gitee.com/dymproject/purest

using PurestAdmin.Application.DictData.Dtos;
using PurestAdmin.Application.DictData.Services;
using PurestAdmin.Core.Consts;



namespace PurestAdmin.Facade.Controllers;
/// <summary>
/// 字典数据接口
/// </summary>
[ApiDescriptionSettings(ApiGroupConst.SYSTEM)]
public class DictDataController : IDynamicApiController
{
    private readonly IDictDataService _dictDataService;
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="dictDataService"></param>
    public DictDataController(IDictDataService dictDataService)
    {
        _dictDataService = dictDataService;
    }

    /// <summary>
    /// 字典数据分页查询
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<PagedList<DictDataProfile>> GetPagedListAsync([FromQuery, Required] GetPagedListInput input)
    {
        return await _dictDataService.GetPagedListAsync(input);
    }

    /// <summary>
    /// 字典数据单条查询
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<DictDataProfile> GetAsync([ApiSeat(ApiSeats.ActionStart)] long id)
    {
        return await _dictDataService.GetAsync(id);
    }

    /// <summary>
    /// 根据字典分类获取字典数据
    /// </summary>
    /// <param name="categoryCode"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<List<DictDataProfile>> GetByCodeAsync([Required, FromQuery] string categoryCode)
    {
        return await _dictDataService.GetListAsync(categoryCode);
    }

    /// <summary>
    /// 字典数据添加
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<long> AddAsync(AddDictDataInput input)
    {
        return await _dictDataService.AddAsync(input);
    }

    /// <summary>
    /// 字典数据编辑
    /// </summary>
    /// <param name="id"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPut]
    public async Task PutAsync([ApiSeat(ApiSeats.ActionStart)] long id, [FromBody] AddDictDataInput input)
    {
        await _dictDataService.PutAsync(id, input);
    }

    /// <summary>
    /// 字典数据删除
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete]
    public async Task DeleteAsync([ApiSeat(ApiSeats.ActionStart)] long id)
    {
        await _dictDataService.DeleteAsync(id);
    }
}
