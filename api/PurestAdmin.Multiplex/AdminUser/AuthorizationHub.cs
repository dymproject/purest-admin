// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

using Flurl;
using Flurl.Http;

using Microsoft.Extensions.Configuration;

using PurestAdmin.Core.ExceptionExtensions;
using PurestAdmin.Multiplex.Contracts.IAdminUser;
using PurestAdmin.Multiplex.Contracts.IAdminUser.Models;

using Volo.Abp.AspNetCore.SignalR;

namespace PurestAdmin.Multiplex.AdminUser;
public class AuthorizationHub(IConfiguration configuration) : AbpHub<IAuthorizationClient>
{
    private readonly IConfiguration _configuration = configuration;


    public void Authorize(string type)
    {
        var authorizationCenters = _configuration.GetValue<List<AuthorizationCenterModel>>("AuthorizationCenter") ?? throw BusinessValidateException.Message("未配置认证中心"); ;
        var authorizationCenter = authorizationCenters?.FirstOrDefault(x => string.Equals(x.Name, type, StringComparison.OrdinalIgnoreCase))
            ?? throw BusinessValidateException.Message("未找到当前认证配置");
        var authorizeUri = string.Empty;
        var tokenUri = string.Empty;
        switch (type)
        {
            case "gitee":
                authorizeUri = "https://gitee.com/oauth/authorize";
                break;
            default:
                break;
        }
        var request = authorizeUri.SetQueryParams(new { client_id = authorizationCenter.ClientId, redirect_uri = authorizationCenter.RedirectUri, response_type = "code", state = Context.ConnectionId });
        Clients.Caller.SendRedirectUri(request.ToString());
    }

    public override Task OnConnectedAsync()
    {
        return Task.CompletedTask;
    }
}
