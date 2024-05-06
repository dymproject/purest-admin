// MIT 许可证
// 版权 © 2023-present https://github.com/dymproject/purest-admin作者 以及贡献者
// 作者或版权持有人都不对任何索赔、损害或其他责任负责，无论这些追责来自合同、侵权或其它行为中，
// 还是产生于、源于或有关于本软件以及本软件的使用或其它处置。

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

using PurestAdmin.Multiplex.Contracts.IAdminUser;

using Volo.Abp.Timing;

namespace PurestAdmin.Multiplex.AdminUser;
public class AdminToken(IConfiguration configuration, IClock clock) : IAdminToken, ISingletonDependency
{
    private readonly IConfiguration _configuration = configuration;
    private readonly IClock _clock = clock;
    /// <summary>
    /// 生成token字符串
    /// </summary>
    /// <param name="claims"></param>
    /// <returns></returns>
    public string GenerateTokenString(IEnumerable<Claim> claims)
    {
        //获取过期分钟数
        var jwtOptions = _configuration.GetRequiredSection("JwtOptions").Get<JwtOptions>() ?? new JwtOptions();
        //取出私钥并以utf8编码字节输出
        var key = Encoding.UTF8.GetBytes(jwtOptions.SecretKey);
        //使用非对称算法对私钥进行加密
        var signingKey = new SymmetricSecurityKey(key);
        //Header,选择签名算法
        var signingAlogorithm = SecurityAlgorithms.HmacSha256;
        //使用HmacSha256来验证加密后的私钥生成数字签名
        var signingCredentials = new SigningCredentials(signingKey, signingAlogorithm);
        //JwtSecurityToken
        var token = new JwtSecurityToken(
            claims: claims,
            notBefore: _clock.Now,
            expires: _clock.Now.AddMinutes(jwtOptions.ExpiredMinutes),
            signingCredentials: signingCredentials
        );
        //生成字符串token
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    /// <summary>
    /// 返回刷新分钟数
    /// </summary>
    /// <returns></returns>
    public double GetRefreshMinutes()
    {
        var jwtOptions = _configuration.GetRequiredSection("JwtOptions").Get<JwtOptions>() ?? new JwtOptions();
        return jwtOptions.RefreshMinutes;
    }
}

internal class JwtOptions
{
    /// <summary>
    /// 密钥
    /// </summary>
    public string SecretKey { get; set; } = "http://www.purestadmin.com";

    /// <summary>
    /// 过期分钟
    /// </summary>
    public double ExpiredMinutes { get; set; } = 60;

    /// <summary>
    /// 刷新分钟数
    /// </summary>
    public double RefreshMinutes { get; set; } = 30;
}
