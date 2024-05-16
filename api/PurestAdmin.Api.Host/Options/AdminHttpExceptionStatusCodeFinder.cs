// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

using System.Net;

using Microsoft.Extensions.Options;

using Volo.Abp;
using Volo.Abp.AspNetCore.ExceptionHandling;

namespace PurestAdmin.Api.Host.Options;
public class AdminHttpExceptionStatusCodeFinder(IOptions<AbpExceptionHttpStatusCodeOptions> options) : DefaultHttpExceptionStatusCodeFinder(options)
{
    public override HttpStatusCode GetStatusCode(HttpContext httpContext, Exception exception)
    {
        if (exception.GetType() == typeof(UserFriendlyException))
        {
            return HttpStatusCode.BadRequest;
        }
        return base.GetStatusCode(httpContext, exception);
    }
}
