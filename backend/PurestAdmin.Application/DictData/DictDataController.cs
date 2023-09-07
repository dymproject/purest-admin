// MIT 许可证
// 版权 © 2023-present dym 和所有贡献者
// 作者或版权持有人都不对任何索赔、损害或其他责任负责，无论这些追责来自合同、侵权或其它行为中，
// 还是产生于、源于或有关于本软件以及本软件的使用或其它处置。

using PurestAdmin.Application.DictData.Dtos;
using PurestAdmin.Application.DictData.Services;



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
