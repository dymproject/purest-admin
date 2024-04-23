// MIT 许可证
// 版权 © 2023-present https://github.com/dymproject/purest-admin作者 以及贡献者
// 作者或版权持有人都不对任何索赔、损害或其他责任负责，无论这些追责来自合同、侵权或其它行为中，
// 还是产生于、源于或有关于本软件以及本软件的使用或其它处置。

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

using PurestAdmin.Application.NoticeServices.Dtos;
using PurestAdmin.Multiplex.AdminUser;
using PurestAdmin.Multiplex.Contracts.IAdminUser;

namespace PurestAdmin.Application.NoticeServices;

/// <summary>
/// 通知公告
/// </summary>
/// <param name="db"></param>
/// <param name="hubContext"></param>
public class NoticeService(ISqlSugarClient db, IHubContext<OnlineUserHub, IOnlineUserClient> hubContext) : ApplicationService
{
    /// <summary>
    /// db
    /// </summary>
    private readonly ISqlSugarClient _db = db;
    /// <summary>
    /// 在线用户Hub
    /// </summary>
    private readonly IHubContext<OnlineUserHub, IOnlineUserClient> _hubContext = hubContext;

    /// <summary>
    /// 分页查询
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task<PagedList<NoticeOutput>> GetPagedListAsync(GetPagedListInput input)
    {
        var pagedList = await _db.Queryable<NoticeEntity>()
            .WhereIF(!input.Title.IsNullOrEmpty(), x => x.Title.Contains(input.Title))
            .WhereIF(input.NoticeType.HasValue, x => x.NoticeType == input.NoticeType)
            .WhereIF(input.Level.HasValue, x => x.Level == input.Level)
            .Select(x => new NoticeEntity
            {
                Id = x.Id.SelectAll(),
                NoticeTypeString = x.NoticeType.GetConfigValue<DictDataEntity>(),
                LevelString = x.Level.GetConfigValue<DictDataEntity>()
            })
            .OrderByDescending(x => x.CreateTime)
            .ToPurestPagedListAsync(input.PageIndex, input.PageSize);
        return pagedList.Adapt<PagedList<NoticeOutput>>();
    }

    /// <summary>
    /// 单条查询
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<NoticeOutput> GetAsync(long id)
    {
        var entity = await _db.Queryable<NoticeEntity>().FirstAsync(x => x.Id == id);
        return entity.Adapt<NoticeOutput>();
    }

    /// <summary>
    /// 添加
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task<long> AddAsync(AddNoticeInput input)
    {
        var entity = input.Adapt<NoticeEntity>();
        return await _db.Insertable(entity).ExecuteReturnSnowflakeIdAsync();
    }

    /// <summary>
    /// 编辑
    /// </summary>
    /// <param name="id"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task PutAsync(long id, EditNoticeInput input)
    {
        var entity = await _db.Queryable<NoticeEntity>().FirstAsync(x => x.Id == id) ?? throw Oops.Bah(ErrorTipsEnum.NoResult);
        var newEntity = input.Adapt(entity);
        _ = await _db.Updateable(newEntity).ExecuteCommandAsync();
    }

    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task DeleteAsync(long id)
    {
        var entity = await _db.Queryable<NoticeEntity>().FirstAsync(x => x.Id == id) ?? throw Oops.Bah(ErrorTipsEnum.NoResult);
        _ = await _db.Updateable(entity).ExecuteCommandAsync();
    }

    /// <summary>
    /// 发送通知
    /// </summary>
    /// <param name="userIds"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task SendAsync(long id, [FromBody] long[] userIds)
    {
        var users = await _db.Queryable<UserEntity>().Where(x => userIds.Contains(x.Id) && x.Status == (int)UserStatusEnum.Normal).ToListAsync();
        var records = users.Select(x => new NoticeRecordEntity()
        {
            Receiver = x.Id,
            NoticeId = id
        });
        await _db.Insertable<NoticeRecordEntity>(records).ExecuteReturnSnowflakeIdListAsync();
    }
}
