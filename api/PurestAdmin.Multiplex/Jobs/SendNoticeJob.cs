// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

using PurestAdmin.Multiplex.AdminUser;
using PurestAdmin.Multiplex.Contracts.IAdminUser;
using PurestAdmin.Multiplex.Contracts.IAdminUser.Models;
using PurestAdmin.Multiplex.Jobs.Args;

using Volo.Abp.BackgroundJobs;
using Volo.Abp.Timing;

namespace PurestAdmin.Multiplex.Jobs;
public class SendNoticeJob(IClock clock, ISqlSugarClient db, IHubContext<OnlineUserHub, IOnlineUserClient> hubContext, ICacheOnlineUser cacheOnlineUser)
    : BackgroundJob<SendNoticeArgs>, ITransientDependency
{
    private readonly IClock _clock = clock;
    /// <summary>
    /// db
    /// </summary>
    private readonly ISqlSugarClient _db = db;
    /// <summary>
    /// 在线用户Hub
    /// </summary>
    private readonly IHubContext<OnlineUserHub, IOnlineUserClient> _hubContext = hubContext;
    /// <summary>
    /// 缓存的在线用户
    /// </summary>
    private readonly ICacheOnlineUser _cacheOnlineUser = cacheOnlineUser;

    public override async void Execute(SendNoticeArgs args)
    {
        var noticeEntity = await _db.Queryable<NoticeEntity>()
            .Where(x => x.Id == args.NoticeId).Select(x => new NoticeEntity
            {
                Id = x.Id.SelectAll(),
                NoticeTypeString = x.NoticeType.GetConfigValue<DictDataEntity>(),
                NoticeTypeEntity = SqlFunc.Subqueryable<DictDataEntity>().Where(d => d.Id == x.NoticeType).First(),
                LevelString = x.Level.GetConfigValue<DictDataEntity>(),
                LevelEntity = SqlFunc.Subqueryable<DictDataEntity>().Where(d => d.Id == x.Level).First()
            }).FirstAsync();

        var noticeRecords = args.UserIds.Select(x => new NoticeRecordEntity
        {
            Receiver = x,
            NoticeId = args.NoticeId
        }).ToList();
        await _db.Insertable(noticeRecords).ExecuteReturnSnowflakeIdListAsync();

        var users = _cacheOnlineUser.GetOnlineUsers().Where(x => args.UserIds.Contains(long.Parse(x.UserId))).ToList();

        var noticeModel = new NoticeItemModel
        {
            Type = noticeEntity.NoticeType.ToString(),
            Title = noticeEntity.Title,
            DateTime = _clock.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            Description = noticeEntity.Content,
            Status = noticeEntity.LevelEntity.Remark,
            Extra = noticeEntity.LevelString
        };
        await _hubContext.Clients.Clients(users.Select(x => x.ConnectionId)).Notice(noticeModel);
    }
}