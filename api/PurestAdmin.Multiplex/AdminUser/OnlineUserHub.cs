// MIT 许可证
// 版权 © 2023-present https://github.com/dymproject/purest-admin作者 以及贡献者
// 作者或版权持有人都不对任何索赔、损害或其他责任负责，无论这些追责来自合同、侵权或其它行为中，
// 还是产生于、源于或有关于本软件以及本软件的使用或其它处置。

using IP2Region.Net.Abstractions;

using Microsoft.AspNetCore.Http.Features;

using PurestAdmin.Multiplex.Contracts.IAdminUser;
using PurestAdmin.Multiplex.Contracts.IAdminUser.Models;

using Volo.Abp.AspNetCore.SignalR;

namespace PurestAdmin.Multiplex.AdminUser;
public class OnlineUserHub(ICacheOnlineUser cacheOnlineUser, ISearcher searcher) : AbpHub<IOnlineUserClient>
{
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

            var onlineUsers = _cacheOnlineUser.GetOnlineUsers();
            if (!onlineUsers.Any(x => x.ConnectionId == Context.ConnectionId))
            {
                var ip = httpContext.Request.Headers["X-Real-IP"].FirstOrDefault() ?? feature.LocalIpAddress.MapToIPv4().ToString();
                var ipString = _searcher.Search(ip)?.Replace("0", "").Replace("|", " ") ?? "暂无";
                var userName = Context.User?.FindFirst("username")?.Value ?? "";
                onlineUsers.Add(new OnlineUserModel
                {
                    ConnectionId = Context.ConnectionId,
                    Ip = ip,
                    IpString = ipString,
                    UserId = Context.UserIdentifier,
                    UserName = userName,
                    ConnectedTime = DateTime.Now,
                });
            }
            Clients.All.UpdateUser(onlineUsers);
            _cacheOnlineUser.SetOnlineUser(onlineUsers);
        }
        return Task.CompletedTask;
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        var httpContext = Context.GetHttpContext();
        if (httpContext != null && Context.UserIdentifier != null)
        {
            var onlineUsers = _cacheOnlineUser.GetOnlineUsers();
            var newOnlineUser = onlineUsers.Where(x => x.ConnectionId != Context.ConnectionId).ToList();
            Clients.All.UpdateUser(newOnlineUser);
            _cacheOnlineUser.SetOnlineUser(newOnlineUser);
        }
        return Task.CompletedTask;
    }
}
