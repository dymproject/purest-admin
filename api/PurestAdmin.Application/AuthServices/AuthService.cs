// MIT 许可证
// 版权 © 2023-present https://github.com/dymproject/purest-admin作者 以及贡献者
// 作者或版权持有人都不对任何索赔、损害或其他责任负责，无论这些追责来自合同、侵权或其它行为中，
// 还是产生于、源于或有关于本软件以及本软件的使用或其它处置。

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

using PurestAdmin.Application.AuthServices.Dtos;
using PurestAdmin.Core.DataEncryption.Encryptions;
using PurestAdmin.Multiplex.Contracts.IAdminUser;

namespace PurestAdmin.Application.SystemServices;
/// <summary>
/// 用户授权服务
/// </summary>
public class AuthService(IConfiguration configuration, IHttpContextAccessor httpContextAccessor, ISqlSugarClient db, ICurrentUser currentUser) : ApplicationService
{
    /// <summary>
    /// configuration
    /// </summary>
    private readonly IConfiguration _configuration = configuration;
    /// <summary>
    /// db
    /// </summary>
    private readonly ISqlSugarClient _db = db;
    /// <summary>
    /// IHttpContextAccessor
    /// </summary>
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    /// <summary>
    /// 当前用户
    /// </summary>
    private readonly ICurrentUser _currentUser = currentUser;

    /// <summary>
    /// 用户登录
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [AllowAnonymous]
    public async Task<LoginOutput> LoginAsync([Required] LoginInput input)
    {
        // 判断用户名或密码是否正确
        var password = MD5Encryption.Encrypt(input.Password);
        var user = await _db.Queryable<UserEntity>().FirstAsync(u => u.Account.Equals(input.Account) && u.Password.Equals(password)) ?? throw Oops.Bah("用户名不存在或用户名密码错误！");
        if (user.Status != (int)UserStatusEnum.Normal) throw Oops.Bah("帐号状态异常，请联系管理员");

        // 映射结果
        var output = user.Adapt<LoginOutput>();

        //Payload,存放用户信息
        var claims = new[]
        {
            new Claim("userid",user.Id.ToString()),
            new Claim("username",user.Name.ToString())
        };
        var expiredTime = int.Parse(_configuration["JWTSettings:ExpiredTime"] ?? "20");
        //取出私钥并以utf8编码字节输出
        var key = Encoding.UTF8.GetBytes(_configuration["JWTSettings:IssuerSigningKey"] ?? "49BA59ABBE56E05749BA59ABBE56E057");
        //使用非对称算法对私钥进行加密
        var signingKey = new SymmetricSecurityKey(key);
        //Header,选择签名算法
        var signingAlogorithm = SecurityAlgorithms.HmacSha256;
        //使用HmacSha256来验证加密后的私钥生成数字签名
        var signingCredentials = new SigningCredentials(signingKey, signingAlogorithm);
        //JwtSecurityToken
        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddMinutes(expiredTime),
            signingCredentials: signingCredentials
        );
        //生成字符串token
        var accessToken = new JwtSecurityTokenHandler().WriteToken(token);

        // 设置 Swagger 刷新自动授权
        _httpContextAccessor.HttpContext.Response.Headers["accesstoken"] = accessToken;

        return output;
    }

    /// <summary>
    /// 获取当前用户信息
    /// </summary>
    /// <returns></returns>
    public async Task<GetUserInfoOutput> GetUserInfoAsync([Required(ErrorMessage = "请输入密码")] string password)
    {
        var claimUserId = _currentUser.Id;
        var user = await _db.Queryable<UserEntity>().FirstAsync(x => x.Id == _currentUser.Id);
        if (!user.Password.Equals(MD5Encryption.Encrypt(password))) throw Oops.Bah("密码错误！");
        return user.Adapt<GetUserInfoOutput>();
    }

    /// <summary>
    /// 获取当前用户的功能
    /// </summary>
    /// <returns></returns>
    public async Task<List<string>> GetFunctionsAsync()
    {
        var functions = await _currentUser.GetFunctionsAsync();
        return functions.Select(x => x.Code).ToList();
    }

    /// <summary>
    /// 修改当前用户信息
    /// </summary>
    /// <returns></returns>
    public async Task PutUserInfoAsync(PutUserInfoInput input)
    {
        var claimUserId = _currentUser.Id;
        var user = await _db.Queryable<UserEntity>().FirstAsync(x => x.Id == _currentUser.Id);
        user = input.Adapt(user);
        user.Password = MD5Encryption.Encrypt(input.Password);
        await _db.Updateable(user).ExecuteCommandAsync();
    }

    /// <summary>
    /// 获取当前用户组织机构树
    /// </summary>
    /// <returns></returns>
    public async Task<List<GetOrganizationTreeOutput>> GetOrganizationTreeAsync()
    {
        var organizationId = _currentUser.Self.OrganizationId;

        var organization = await _db.Queryable<OrganizationEntity>().FirstAsync(x => x.Id == organizationId) ?? throw Oops.Bah("无法找到当前登录用户的组织机构，请联系管理检查数据");

        var organizationChildren = await _db.Queryable<OrganizationEntity>().OrderByDescending(x => x.Sort).ToTreeAsync(x => x.Children, x => x.ParentId, organizationId);

        if (organizationChildren != null)
        {
            organization.Children = organizationChildren;
        }
        var result = new List<OrganizationEntity>() { organization };
        return result.Adapt<List<GetOrganizationTreeOutput>>();
    }

    /// <summary>
    /// 获得当前平台信息
    /// </summary>
    /// <returns></returns>
    public GetSystemPlatformInfoOutput GetSystemPlatformInfoAsync()
    {
        return new GetSystemPlatformInfoOutput();
    }
}
