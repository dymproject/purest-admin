// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;

namespace PurestAdmin.Api.Host.Options;
public class AuthorizationMiddlewareResultHandler : IAuthorizationMiddlewareResultHandler
{
    public async Task HandleAsync(RequestDelegate next, HttpContext context, AuthorizationPolicy policy, PolicyAuthorizationResult authorizeResult)
    {
        if (authorizeResult.Challenged)
        {
            await context.ChallengeAsync();
            return;
        }
        else if (authorizeResult.Forbidden)
        {
            await context.ForbidAsync();
            return;
        }
        await next(context);
    }
}
