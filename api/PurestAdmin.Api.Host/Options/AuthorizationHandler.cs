// MIT 许可证
// 版权 © 2023-present https://github.com/dymproject/purest-admin作者 以及贡献者
// 作者或版权持有人都不对任何索赔、损害或其他责任负责，无论这些追责来自合同、侵权或其它行为中，
// 还是产生于、源于或有关于本软件以及本软件的使用或其它处置。

using System.IdentityModel.Tokens.Jwt;

using Microsoft.AspNetCore.Authorization;

using PurestAdmin.Multiplex.Contracts.IAdminUser;

using Volo.Abp.Timing;

namespace PurestAdmin.Api.Host.Options;
public class AuthorizationHandler(IHostEnvironment hostEnvironment, ICurrentUser currentUser, IAdminToken adminToken, IClock clock) : IAuthorizationHandler
{
    private readonly IHostEnvironment _hostEnvironment = hostEnvironment;
    private readonly ICurrentUser _currentUser = currentUser;
    private readonly IAdminToken _adminToken = adminToken;
    private readonly IClock _clock = clock;

    public async Task HandleAsync(AuthorizationHandlerContext context)
    {
        if (context.Resource is HttpContext httpContext)
        {
            var isAuthenticated = httpContext.User.Identity?.IsAuthenticated ?? false;
            if (!isAuthenticated)
            {
                context.Fail();
                return;
            }
            var accessToken = httpContext.Request.Headers.Authorization.ToString().Replace("Bearer ", "");
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = jwtSecurityTokenHandler.ReadJwtToken(accessToken);
            var expires = jwtSecurityToken.ValidTo;
            var refreshMinutes = _adminToken.GetRefreshMinutes();
            if ((expires - _clock.Now.ToUniversalTime()).TotalMinutes < refreshMinutes)
            {
                //颁发新的token
                var token = _adminToken.GenerateTokenString(jwtSecurityToken.Payload.Claims.ToArray());
                httpContext.Response.Headers["accesstoken"] = token;
            }
            //if (_hostEnvironment.IsProduction() && !httpContext.Request.Method.Equals("get", StringComparison.OrdinalIgnoreCase))
            //{
            //    context.Fail();
            //    return;
            //}
            //var endpoint = httpContext.GetEndpoint() as RouteEndpoint;
            //var pattern = endpoint?.RoutePattern;
            //if (!_hostEnvironment.IsDevelopment())
            //{
            //    var interfaces = await _currentUser.GetInterfacesAsync();
            //    var ownInterface = interfaces.Any(x => x.Path == $"/{pattern?.RawText}" && x.RequestMethod.Equals(httpContext.Request.Method, StringComparison.CurrentCultureIgnoreCase));
            //    if (!ownInterface)
            //    {
            //        context.Fail();
            //        return;
            //    }
            //}
        }
        await Task.CompletedTask;
    }
}
