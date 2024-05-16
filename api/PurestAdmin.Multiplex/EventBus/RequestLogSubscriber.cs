// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

using Mapster;

using Volo.Abp.EventBus;

namespace PurestAdmin.Multiplex.EventBus;
/// <summary>
/// 请求日志订阅
/// </summary>
public class RequestLogSubscriber : ILocalEventHandler<RequestLogEventData>,
          ITransientDependency
{
    public readonly ISqlSugarClient _client;
    public RequestLogSubscriber(ISqlSugarClient client)
    {
        _client = client;
    }
    public async Task HandleEventAsync(RequestLogEventData eventData)
    {
        var entity = eventData.Adapt<RequestLogEntity>();
        await _client.Insertable(entity).ExecuteReturnSnowflakeIdAsync();
    }
}

public class RequestLogEventData
{
    /// <summary>
    /// 控制器
    /// </summary>
    public string ControllerName { get; set; }
    /// <summary>
    /// 方法名
    /// </summary>
    public string ActionName { get; set; }
    /// <summary>
    /// 请求类型
    /// </summary>
    public string RequestMethod { get; set; }
    /// <summary>
    /// 服务器环境
    /// </summary>
    public string EnvironmentName { get; set; }
    /// <summary>
    /// 完成情况
    /// </summary>
    public bool IsSuccess { get; set; }
    /// <summary>
    /// 执行耗时
    /// </summary>
    public long ElapsedTime { get; set; }
    /// <summary>
    /// 客户端IP
    /// </summary>
    public string ClientIp { get; set; }
}
