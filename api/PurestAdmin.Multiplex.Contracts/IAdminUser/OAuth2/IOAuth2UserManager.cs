// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

using PurestAdmin.SqlSugar.Entity;

namespace PurestAdmin.Multiplex.Contracts.IAdminUser.OAuth2;
public interface IOAuth2UserManager
{
    /// <summary>
    /// 持久化OAuth认证中心的数据
    /// </summary>
    /// <param name="userInfo"></param>
    /// <returns></returns>
    Task<OAuth2UserEntity> GetOAuth2UserPersistenceIdAsync(OAuth2UserInfo userInfo);
}
