// MIT 许可证
// 版权 © 2023-present https://github.com/dymproject/purest-admin作者 以及贡献者
// 作者或版权持有人都不对任何索赔、损害或其他责任负责，无论这些追责来自合同、侵权或其它行为中，
// 还是产生于、源于或有关于本软件以及本软件的使用或其它处置。

using Microsoft.AspNetCore.Http.Features;

using PurestAdmin.Core.CacheExtensions;

using Volo.Abp.AspNetCore.SignalR;

namespace PurestAdmin.Multiplex.Signalr.OnlineUser;
public class OnlineUserHub(IPurestCache cache) : AbpHub<IOnlineUserClient>
{
    private readonly IPurestCache _cache = cache;
    private const string USER_KEY = "online_user";

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
                var remoteAddress = feature.RemoteIpAddress;
                var userName = Context.User?.FindFirst("username")?.Value ?? "";
                onlineUsers.Add(new OnlineUserModel
                {
                    ConnectionId = Context.ConnectionId,
                    Ip = remoteAddress.ToString(),
                    UserId = Context.UserIdentifier,
                    UserName = userName
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
