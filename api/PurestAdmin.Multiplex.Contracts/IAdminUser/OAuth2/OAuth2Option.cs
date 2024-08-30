// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

namespace PurestAdmin.Multiplex.Contracts.IAdminUser.OAuth2;
public class OAuth2Option
{
    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// id 
    /// </summary>
    public string ClientId { get; set; }
    /// <summary>
    /// 密码
    /// </summary>
    public string ClientSecret { get; set; }
    /// <summary>
    /// 回调地址
    /// </summary>
    public string RedirectUri { get; set; }
}
