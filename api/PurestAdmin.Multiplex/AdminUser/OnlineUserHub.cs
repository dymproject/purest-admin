// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

using IP2Region.Net.Abstractions;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Features;

using PurestAdmin.Multiplex.Contracts.IAdminUser;
using PurestAdmin.Multiplex.Contracts.IAdminUser.Models;

using Volo.Abp.AspNetCore.SignalR;
using Volo.Abp.Timing;

namespace PurestAdmin.Multiplex.AdminUser;
[Authorize]
public class OnlineUserHub(IClock clock, ICacheOnlineUser cacheOnlineUser, ISearcher searcher) : AbpHub<IOnlineUserClient>
{
    private readonly IClock _clock = clock;
    private readonly ICacheOnlineUser _cacheOnlineUser = cacheOnlineUser;
    private readonly ISearcher _searcher = searcher;

    /// <summary>
    /// 给客户端同步在线用户
    /// </summary>
    public void GetOnlineUsers()
    {
        var onlineUsers = _cacheOnlineUser.GetOnlineUsers();
        Clients.Caller.UpdateUser([.. onlineUsers.OrderBy(x => x.UserId)]);
    }

    /// <summary>
    /// 给用户发送消息
    /// </summary>
    public void SendMessage(string connectionsIds, string message)
    {
        Clients.Clients(connectionsIds).Message(message);
    }

    /// <summary>
    /// 强制下线某个用户
    /// </summary>
    /// <param name="connectionId"></param>
    public void OfflineUser(string connectionId)
    {
        Clients.Client(connectionId).Logout();
    }

    public override Task OnConnectedAsync()
    {
        var isAuthenticated = Context.User?.Identity?.IsAuthenticated;
        if (isAuthenticated.HasValue && isAuthenticated.Value)
        {
            var feature = Context.Features.Get<IHttpConnectionFeature>();
            var httpContext = Context.GetHttpContext();
            ArgumentNullException.ThrowIfNull(feature);
            ArgumentNullException.ThrowIfNull(httpContext);
            ArgumentNullException.ThrowIfNull(Context.UserIdentifier);

            _cacheOnlineUser.TryUpdate(users =>
            {
                var ip = httpContext.Request.Headers["X-Real-IP"].FirstOrDefault() ?? feature.LocalIpAddress.MapToIPv4().ToString();
                var ipString = _searcher.Search(ip)?.Replace("0", "").Replace("|", " ") ?? "暂无";
                var userName = Context.User?.FindFirst("username")?.Value ?? "";
                users.Add(new OnlineUserModel
                {
                    ConnectionId = Context.ConnectionId,
                    Ip = ip,
                    IpString = ipString,
                    UserId = Context.UserIdentifier,
                    UserName = userName,
                    ConnectedTime = _clock.Now,
                });
                Clients.All.UpdateUser(users);
                return users;
            });
        }
        return Task.CompletedTask;
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        _cacheOnlineUser.TryUpdate(users =>
        {
            var newUsers = users.Where(u => u.ConnectionId != Context.ConnectionId).ToList();
            Clients.All.UpdateUser(newUsers);
            return newUsers;
        });
        return Task.CompletedTask;
    }
}
