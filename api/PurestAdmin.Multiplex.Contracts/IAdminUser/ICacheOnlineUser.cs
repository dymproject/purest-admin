// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

using PurestAdmin.Multiplex.Contracts.IAdminUser.Models;

namespace PurestAdmin.Multiplex.Contracts.IAdminUser;
public interface ICacheOnlineUser
{
    List<OnlineUserModel> GetOnlineUsers();

    void SetOnlineUser(List<OnlineUserModel> users);
}
