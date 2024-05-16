// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

using System.Security.Claims;

namespace PurestAdmin.Multiplex.Contracts.IAdminUser;
public interface IAdminToken
{
    /// <summary>
    /// 生成token字符串
    /// </summary>
    /// <param name="claims"></param>
    /// <returns></returns>
    string GenerateTokenString(IEnumerable<Claim> claims);
    /// <summary>
    /// 返回刷新分钟数
    /// </summary>
    /// <returns></returns>
    double GetRefreshMinutes();
}
