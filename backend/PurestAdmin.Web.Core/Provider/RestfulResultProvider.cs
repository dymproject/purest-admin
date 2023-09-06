// PurestAdmin
// Author:dym
// MIT License
// https://gitee.com/dymproject/purest

using System;
using System.Threading.Tasks;

using Furion;
using Furion.DataValidation;
using Furion.DependencyInjection;
using Furion.FriendlyException;
using Furion.UnifyResult;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Hosting;

using StackExchange.Profiling.Internal;

namespace PurestAdmin.Web.Core.Provider;

/// <summary>
/// 规范化RESTful风格返回值
/// </summary>
[SuppressSniffer, UnifyModel(typeof(RestfulResult<>))]
public class RestfulResultProvider : IUnifyResultProvider
{
    public IActionResult OnException(ExceptionContext context, ExceptionMetadata metadata)
    {
        // 解析异常信息
        var exceptionMetadata = UnifyContext.GetExceptionMetadata(context);

        return new JsonResult(new RestfulResult<object>
        {
            Code = exceptionMetadata.StatusCode,
            Success = false,
            Result = null,
            Message = exceptionMetadata.Errors,
            Extras = UnifyContext.Take(),
            Timestamp = DateTime.Now.Millisecond
        });
    }

    public IActionResult OnSucceeded(ActionExecutedContext context, object data)
    {
        switch (context.Result)
        {
            // 处理内容结果
            case ContentResult contentResult:
                data = contentResult.Content;
                break;
            // 处理对象结果
            case ObjectResult objectResult:
                data = objectResult.Value;
                break;
            //空结果
            case EmptyResult:
                data = null;
                break;
            //默认返回空
            default:
                return null;
        }

        return new JsonResult(new RestfulResult<object>
        {
            Code = context.Result is EmptyResult ? StatusCodes.Status204NoContent : StatusCodes.Status200OK,  // 处理没有返回值情况 204
            Success = true,
            Result = data,
            Message = "请求成功",
            Extras = UnifyContext.Take(),
            Timestamp = DateTime.Now.Millisecond
        });
    }

    public IActionResult OnValidateFailed(ActionExecutingContext context, ValidationMetadata metadata)
    {
        return new JsonResult(new RestfulResult<object>
        {
            Code = StatusCodes.Status400BadRequest,
            Success = false,
            Result = null,
            Message = metadata.ValidationResult.ToJson(),
            Extras = UnifyContext.Take(),
            Timestamp = DateTime.Now.Millisecond
        });
    }

    public async Task OnResponseStatusCodes(HttpContext context, int statusCode, UnifyResultSettingsOptions unifyResultSettings = null)
    {
        // 设置响应状态码
        UnifyContext.SetResponseStatusCodes(context, statusCode, unifyResultSettings);

        switch (statusCode)
        {
            // 处理 401 状态码
            case StatusCodes.Status401Unauthorized:
                await context.Response.WriteAsJsonAsync(new RestfulResult<object>
                {
                    Code = StatusCodes.Status401Unauthorized,
                    Success = false,
                    Result = null,
                    Message = "401 未经授权",
                    Extras = UnifyContext.Take(),
                    Timestamp = DateTime.Now.Millisecond
                });
                break;
            // 处理 403 状态码
            case StatusCodes.Status403Forbidden:
                await context.Response.WriteAsJsonAsync(new RestfulResult<object>
                {
                    Code = StatusCodes.Status403Forbidden,
                    Success = false,
                    Result = null,
                    Message = "403 接口未授权",
                    Extras = UnifyContext.Take(),
                    Timestamp = DateTime.Now.Millisecond
                });
                break;

            default: break;
        }
    }
}

/// <summary>
/// RESTful风格返回格式
/// </summary>
/// <typeparam name="T"></typeparam>
[SuppressSniffer]
public class RestfulResult<T>
{
    /// <summary>
    /// 执行成功
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// 状态码
    /// </summary>
    public int? Code { get; set; }

    /// <summary>
    /// 错误信息
    /// </summary>
    public object Message { get; set; }

    /// <summary>
    /// 数据
    /// </summary>
    public T Result { get; set; }

    /// <summary>
    /// 附加数据
    /// </summary>
    public object Extras { get; set; }

    /// <summary>
    /// 时间戳
    /// </summary>
    public long Timestamp { get; set; }
}