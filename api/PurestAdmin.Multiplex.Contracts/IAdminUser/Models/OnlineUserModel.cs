// MIT 许可证
// 版权 © 2023-present https://github.com/dymproject/purest-admin作者 以及贡献者
// 作者或版权持有人都不对任何索赔、损害或其他责任负责，无论这些追责来自合同、侵权或其它行为中，
// 还是产生于、源于或有关于本软件以及本软件的使用或其它处置。

namespace PurestAdmin.Multiplex.Contracts.IAdminUser.Models;
public class OnlineUserModel
{
    /// <summary>
    /// 连接Id
    /// </summary>
    public string ConnectionId { get; set; }
    /// <summary>
    /// 用户Id 
    /// </summary>
    public string UserId { get; set; }
    /// <summary>
    /// 用户名
    /// </summary>
    public string UserName { get; set; }
    /// <summary>
    /// ip
    /// </summary>
    public string Ip { get; set; }
    /// <summary>
    /// ip属地
    /// </summary>
    public string IpString { get; set; }
    /// <summary>
    /// 链接时间
    /// </summary>
    public DateTime ConnectedTime { get; set; }
}
