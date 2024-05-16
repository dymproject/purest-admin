// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

using PurestAdmin.Multiplex.Contracts.IAdminUser.Models;

namespace PurestAdmin.Multiplex.Contracts.IAdminUser;
public interface IOnlineUserClient
{
    /// <summary>
    /// 更新在线列表
    /// </summary>
    /// <param name="users"></param>
    /// <returns></returns>
    Task UpdateUser(List<OnlineUserModel> users);
    /// <summary>
    /// 发送通知
    /// </summary>
    /// <param name="notices"></param>
    /// <returns></returns>
    Task Notice(NoticeItemModel noticeItem);
    /// <summary>
    /// 发送消息
    /// </summary>
    /// <param name="notices"></param>
    /// <returns></returns>
    Task Message(string message);
    /// <summary>
    /// 客户端退出
    /// </summary>
    /// <returns></returns>
    Task Logout();
}
