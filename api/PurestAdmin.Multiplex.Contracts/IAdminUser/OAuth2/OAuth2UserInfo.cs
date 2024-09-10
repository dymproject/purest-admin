// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

namespace PurestAdmin.Multiplex.Contracts.IAdminUser.OAuth2;
/// <summary>
/// 统一auth2用户信息
/// </summary>
public class OAuth2UserInfo
{
    /// <summary>
    /// Id
    /// </summary>
    public long Id { get; set; }
    /// <summary>
    /// Name
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// Type
    /// </summary>
    public string Type { get; set; }
}
