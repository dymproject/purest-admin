// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

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
