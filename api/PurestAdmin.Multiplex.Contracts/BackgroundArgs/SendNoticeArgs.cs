// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

namespace PurestAdmin.Multiplex.Contracts.BackgroundArgs;
public class SendNoticeArgs
{
    /// <summary>
    /// 通知公告Id
    /// </summary>
    public long NoticeId { get; set; }
    /// <summary>
    /// 用户ID集合
    /// </summary>
    public List<long> UserIds { get; set; }
}
