// MIT 许可证
// 版权 © 2023-present https://github.com/dymproject/purest-admin作者 以及贡献者
// 作者或版权持有人都不对任何索赔、损害或其他责任负责，无论这些追责来自合同、侵权或其它行为中，
// 还是产生于、源于或有关于本软件以及本软件的使用或其它处置。

namespace PurestAdmin.Multiplex.Jobs.Args;
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
