// MIT 许可证
// 版权 © 2023-present dym 和所有贡献者
// 作者或版权持有人都不对任何索赔、损害或其他责任负责，无论这些追责来自合同、侵权或其它行为中，
// 还是产生于、源于或有关于本软件以及本软件的使用或其它处置。

using PurestAdmin.Application.User.Dtos;
using PurestAdmin.Application.User.Services;

namespace PurestAdmin.Facade.Controllers;
/// <summary>
/// 用户接口
/// </summary>
[ApiDescriptionSettings(ApiGroupConst.SYSTEM)]
public class UserController : IDynamicApiController
{
    private readonly IUserService _userService;
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="userService"></param>
    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    /// <summary>
    /// 分页查询
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpGet, ApiDescriptionSettings(Name = "page")]
    public async Task<PagedList<UserProfile>> GetPagedListAsync([FromQuery, Required] GetPagedListInput input)
    {
        return await _userService.GetPagedListAsync(input);
    }

    /// <summary>
    /// 单条查询
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<UserProfile> GetAsync(long id)
    {
        return await _userService.GetAsync(id);
    }

    /// <summary>
    /// 添加
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPost, UnitOfWork]
    public async Task<long> AddAsync(AddUserInput input)
    {
        return await _userService.AddAsync(input);
    }

    /// <summary>
    /// 编辑
    /// </summary>
    /// <param name="id"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    [HttpPut]
    public async Task PutAsync(long id, EditUserInput input)
    {
        await _userService.PutAsync(id, input);
    }

    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete]
    public async Task DeleteAsync(long id)
    {
        await _userService.DeleteAsync(id);
    }

    /// <summary>
    /// 重置密码
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<string> ResetPassword([ApiSeat(ApiSeats.ActionStart)] long id)
    {
        return await _userService.ResetPassword(id);
    }
}
