// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

using PurestAdmin.Multiplex.Contracts.IAdminUser.Models;

namespace PurestAdmin.Multiplex.Contracts.IAdminUser;
public interface ICacheOnlineUser
{
    List<OnlineUserModel> GetOnlineUsers();

    bool TryUpdate(Func<List<OnlineUserModel>, List<OnlineUserModel>> updateFunc);

    void SetOnlineUser(List<OnlineUserModel> users);
}
