// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

using System.Text;

using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;

using Volo.Abp;
using Volo.Abp.AspNetCore.ExceptionHandling;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.ExceptionHandling;
using Volo.Abp.Http;
using Volo.Abp.Json;

namespace PurestAdmin.Api.Host.Options;

public class AdminAbpExceptionsFilter : AbpExceptionFilter
{
    protected override void LogException(ExceptionContext context, out RemoteServiceErrorInfo remoteServiceErrorInfo)
    {
        var exceptionHandlingOptions = context.GetRequiredService<IOptions<AbpExceptionHandlingOptions>>().Value;
        var exceptionToErrorInfoConverter = context.GetRequiredService<IExceptionToErrorInfoConverter>();
        remoteServiceErrorInfo = exceptionToErrorInfoConverter.Convert(context.Exception, options =>
        {
            options.SendExceptionsDetailsToClients = exceptionHandlingOptions.SendExceptionsDetailsToClients;
            options.SendStackTraceToClients = exceptionHandlingOptions.SendStackTraceToClients;
        });
        if (context.Exception.GetType() != typeof(UserFriendlyException))
        {
            var remoteServiceErrorInfoBuilder = new StringBuilder();
            remoteServiceErrorInfoBuilder.AppendLine(context.GetRequiredService<IJsonSerializer>().Serialize(remoteServiceErrorInfo, indented: true));

            var logger = context.GetService<ILogger<AbpExceptionFilter>>();
            var logLevel = context.Exception.GetLogLevel();
            logger?.LogWithLevel(logLevel, remoteServiceErrorInfoBuilder.ToString());
        }
    }
}
