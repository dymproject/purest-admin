// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

namespace PurestAdmin.Multiplex.Contracts.IAdminUser.OAuth2;
public interface IAuthorizationClient
{
    /// <summary>
    /// 通知打开认证平台页面
    /// </summary>
    /// <returns></returns>
    Task NoticeOpenAuthorizationPage(string url);
    /// <summary>
    /// 通知异常
    /// </summary>
    /// <returns></returns>
    Task NoticeException(string message);
    /// <summary>
    /// 通知需要注册
    /// </summary>
    /// <returns></returns>
    Task NoticeRegister(long authId);
    /// <summary>
    /// 通知页面重定向
    /// </summary>
    /// <returns></returns>
    Task NoticeRedirect(string token, List<string> functions);
}
