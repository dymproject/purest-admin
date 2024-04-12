// MIT 许可证
// 版权 © 2023-present https://github.com/dymproject/purest-admin作者 以及贡献者
// 作者或版权持有人都不对任何索赔、损害或其他责任负责，无论这些追责来自合同、侵权或其它行为中，
// 还是产生于、源于或有关于本软件以及本软件的使用或其它处置。

using IP2Region.Net.Abstractions;

using Microsoft.AspNetCore.Http.Features;

using Volo.Abp.AspNetCore.SignalR;

namespace PurestAdmin.Multiplex.SignalrHubs.OnlineUser;
public class OnlineUserHub(IPurestCache cache, ISearcher searcher) : AbpHub<IOnlineUserClient>
{
    private readonly IPurestCache _cache = cache;
    private readonly ISearcher _searcher = searcher;
    private const string USER_KEY = "online_user";

    /// <summary>
    /// 给客户端同步在线用户
    /// </summary>
    public void GetOnlineUsers()
    {
        var onlineUsers = _cache.Get<List<OnlineUserModel>>(USER_KEY) ?? [];
        Clients.Caller.UpdateUser([.. onlineUsers.OrderBy(x => x.UserId)]);
    }

    /// <summary>
    /// 发送Notice
    /// </summary>
    public void SendNotice(string connectionsIds, NoticeModel[] model)
    {
        Clients.Clients(connectionsIds).Notice([.. model]);
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

            var onlineUsers = _cache.Get<List<OnlineUserModel>>(USER_KEY) ?? [];
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
            _cache.Set(USER_KEY, onlineUsers);
        }
        return Task.CompletedTask;
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        var httpContext = Context.GetHttpContext();
        if (httpContext != null && Context.UserIdentifier != null)
        {
            var onlineUsers = _cache.Get<List<OnlineUserModel>>(USER_KEY) ?? [];
            var newOnlineUser = onlineUsers.Where(x => x.ConnectionId != Context.ConnectionId).ToList();
            Clients.All.UpdateUser(newOnlineUser);
            _cache.Set(USER_KEY, newOnlineUser);
        }
        return Task.CompletedTask;
    }
}
