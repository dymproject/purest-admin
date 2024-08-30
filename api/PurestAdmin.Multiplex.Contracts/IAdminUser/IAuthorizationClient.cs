// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

namespace PurestAdmin.Multiplex.Contracts.IAdminUser;
public interface IAuthorizationClient
{
    /// <summary>
    /// SendRedirectUri
    /// </summary>
    /// <returns></returns>
    Task SendRedirectUri(string uri);
    /// <summary>
    /// 通知重定向
    /// </summary>
    /// <returns></returns>
    Task NoticeRedirect();
}
