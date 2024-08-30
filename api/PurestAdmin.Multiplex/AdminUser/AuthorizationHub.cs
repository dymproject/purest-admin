// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

using Microsoft.Extensions.Configuration;

using PurestAdmin.Core.ExceptionExtensions;
using PurestAdmin.Multiplex.Contracts.Consts;
using PurestAdmin.Multiplex.Contracts.IAdminUser.OAuth2;

using Volo.Abp.AspNetCore.SignalR;

namespace PurestAdmin.Multiplex.AdminUser;
public class AuthorizationHub(IConfiguration configuration) : AbpHub<IAuthorizationClient>
{
    private readonly IConfiguration _configuration = configuration;


    public void Authorize(string type)
    {
        var authorizationCenters = _configuration.GetRequiredSection("OAuth2Options").Get<List<OAuth2Option>>() ?? throw BusinessValidateException.Message("未配置认证中心");
        var authorizationCenter = authorizationCenters?.FirstOrDefault(x => string.Equals(x.Name, type, StringComparison.OrdinalIgnoreCase))
            ?? throw BusinessValidateException.Message("未找到当前认证配置");
        string authorizeUrl = string.Empty;
        switch (type)
        {
            case OAuth2TypeConst.GITEE:
                authorizeUrl = $"https://gitee.com/oauth/authorize?client_id={authorizationCenter.ClientId}&redirect_uri={authorizationCenter.RedirectUri}&response_type=code&state={Context.ConnectionId}";
                break;
            default:
                break;
        }
        Clients.Caller.NoticeOpenAuthorizationPage(authorizeUrl);
    }

    public override Task OnConnectedAsync()
    {
        return Task.CompletedTask;
    }
}
