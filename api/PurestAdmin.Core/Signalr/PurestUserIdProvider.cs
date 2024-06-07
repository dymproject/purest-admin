// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

using System.Security.Claims;

using Microsoft.AspNetCore.SignalR;

namespace PurestAdmin.Core.Signalr;
public class PurestUserIdProvider : IUserIdProvider
{
    public virtual string? GetUserId(HubConnectionContext connection)
    {
        return connection.User.FindFirstValue("userid");
    }
}
