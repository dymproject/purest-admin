

// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

using System.Reflection;
using System.Text.Json;

using Microsoft.Extensions.DependencyInjection;

using Newtonsoft.Json;

using Volo.Abp.Timing;

namespace PurestAdmin.Workflow;
internal static class ExtensionMethods
{
    // 使用 Objects 而不是 All，这样数组会序列化为 [...] 而不是 {"$type":"...", "$values":[...]}
    private static readonly JsonSerializerSettings SerializerSettings = new() { TypeNameHandling = TypeNameHandling.Objects };

    /// <summary>
    /// 将 System.Text.Json 的 JsonElement 转换为普通 .NET 对象
    /// </summary>
    private static object ConvertJsonElement(object value)
    {
        if (value is JsonElement element)
        {
            return element.ValueKind switch
            {
                JsonValueKind.Array => element.EnumerateArray().Select(e => ConvertJsonElement(e)).ToList(),
                JsonValueKind.Object => element.EnumerateObject().ToDictionary(p => p.Name, p => ConvertJsonElement(p.Value)),
                JsonValueKind.String => element.GetString(),
                JsonValueKind.Number => element.TryGetInt64(out var l) ? l : element.GetDouble(),
                JsonValueKind.True => true,
                JsonValueKind.False => false,
                JsonValueKind.Null => null,
                JsonValueKind.Undefined => null,
                _ => null
            };
        }
        return value;
    }

    /// <summary>
    /// 原地转换字典中的 JsonElement，保持原字典类型不变
    /// </summary>
    private static void ConvertJsonElementsInPlace(IDictionary<string, object> dict)
    {
        var keys = dict.Keys.ToList();
        foreach (var key in keys)
        {
            var value = dict[key];
            if (value is JsonElement)
            {
                dict[key] = ConvertJsonElement(value);
            }
        }
    }

    internal static WfWorkflowEntity ToPersistable(this WorkflowInstance instance, WfWorkflowEntity? persistable = null)
    {
        if (persistable == null)
            persistable = new WfWorkflowEntity();

        // 原地转换 JsonElement，保持 GeneralAuditingDefinition 类型不变
        if (instance.Data is IDictionary<string, object> dataDict)
        {
            ConvertJsonElementsInPlace(dataDict);
        }

        persistable.Data = JsonConvert.SerializeObject(instance.Data, SerializerSettings);  //解决了前端表单数据转化成json出现的问题，问题表现为表单中的复选框数据无法正常显示。望作者采纳
        persistable.Description = instance.Description;
        persistable.Reference = instance.Reference;
        persistable.InstanceId = instance.Id;
        persistable.NextExecution = instance.NextExecution;
        persistable.Version = instance.Version;
        persistable.WorkflowDefinitionId = instance.WorkflowDefinitionId;
        persistable.Status = (int)instance.Status;
        persistable.CreateTime = instance.CreateTime;
        persistable.CompleteTime = instance.CompleteTime;

        foreach (var ep in instance.ExecutionPointers)
        {
            var persistedEP = persistable.ExecutionPointers?.FirstOrDefault(x => x.Id == ep.Id);

            if (persistedEP == null)
            {
                persistedEP = new WfExecutionPointerEntity();
                persistedEP.Id = ep.Id ?? Guid.NewGuid().ToString();
                if (persistable.ExecutionPointers == null)
                {
                    persistable.ExecutionPointers = [persistedEP];
                }
                else
                {
                    persistable.ExecutionPointers.Add(persistedEP);
                }
            }

            persistedEP.StepId = ep.StepId;
            persistedEP.Active = ep.Active;
            persistedEP.SleepUntil = ep.SleepUntil;
            persistedEP.PersistenceData = JsonConvert.SerializeObject(ep.PersistenceData, SerializerSettings);
            persistedEP.StartTime = ep.StartTime;
            persistedEP.EndTime = ep.EndTime;
            persistedEP.StepName = ep.StepName;
            persistedEP.RetryCount = ep.RetryCount;
            persistedEP.PredecessorId = ep.PredecessorId;
            persistedEP.ContextItem = JsonConvert.SerializeObject(ep.ContextItem, SerializerSettings);
            persistedEP.Children = string.Empty;

            foreach (var child in ep.Children)
                persistedEP.Children += child + ";";

            persistedEP.EventName = ep.EventName;
            persistedEP.EventKey = ep.EventKey;
            persistedEP.EventPublished = ep.EventPublished;
            persistedEP.EventData = JsonConvert.SerializeObject(ep.EventData, SerializerSettings);
            persistedEP.Outcome = JsonConvert.SerializeObject(ep.Outcome, SerializerSettings);
            persistedEP.Status = (int)ep.Status;

            persistedEP.Scope = string.Empty;
            foreach (var item in ep.Scope)
                persistedEP.Scope += item + ";";

            foreach (var attr in ep.ExtensionAttributes)
            {
                var persistedAttr = persistedEP.ExtensionAttributes?.FirstOrDefault(x => x.AttributeKey == attr.Key);
                if (persistedAttr == null)
                {
                    persistedAttr = new WfExecutionAttributeEntity();
                    if (persistedEP.ExtensionAttributes == null)
                    {
                        persistedEP.ExtensionAttributes = [persistedAttr];
                    }
                    else
                    {
                        persistedEP.ExtensionAttributes.Add(persistedAttr);
                    }
                }

                persistedAttr.AttributeKey = attr.Key;
                persistedAttr.AttributeValue = JsonConvert.SerializeObject(attr.Value, SerializerSettings);
            }
        }

        return persistable;
    }

    internal static WfExecutionErrorEntity ToPersistable(this ExecutionError instance)
    {
        var result = new WfExecutionErrorEntity();
        result.ErrorTime = instance.ErrorTime;
        result.Message = instance.Message;
        result.ExecutionPointerId = instance.ExecutionPointerId;
        result.WorkflowId = instance.WorkflowId;

        return result;
    }

    internal static WfSubscriptionEntity ToPersistable(this EventSubscription instance, IClock clock)
    {
        WfSubscriptionEntity result = new WfSubscriptionEntity();
        result.SubscriptionId = instance.Id;
        result.EventKey = instance.EventKey;
        result.EventName = instance.EventName;
        result.StepId = instance.StepId;
        result.ExecutionPointerId = instance.ExecutionPointerId;
        result.WorkflowId = instance.WorkflowId;
        result.SubscribeAsOf = DateTime.SpecifyKind(instance.SubscribeAsOf, clock.Kind);
        result.SubscriptionData = JsonConvert.SerializeObject(instance.SubscriptionData, SerializerSettings);
        result.ExternalToken = instance.ExternalToken;
        result.ExternalTokenExpiry = instance.ExternalTokenExpiry;
        result.ExternalWorkerId = instance.ExternalWorkerId;

        return result;
    }

    internal static WfEventEntity ToPersistable(this Event instance, IClock clock)
    {
        WfEventEntity result = new WfEventEntity();
        result.EventId = instance.Id;
        result.EventKey = instance.EventKey;
        result.EventName = instance.EventName;
        result.EventTime = DateTime.SpecifyKind(instance.EventTime, clock.Kind);
        result.IsProcessed = instance.IsProcessed;
        result.EventData = JsonConvert.SerializeObject(instance.EventData, SerializerSettings);

        return result;
    }

    internal static WfScheduledCommandEntity ToPersistable(this ScheduledCommand instance)
    {
        var result = new WfScheduledCommandEntity();
        result.CommandName = instance.CommandName;
        result.Data = instance.Data;
        result.ExecuteTime = instance.ExecuteTime;

        return result;
    }

    internal static WorkflowInstance ToWorkflowInstance(this WfWorkflowEntity instance, IClock clock)
    {
        WorkflowInstance result = new WorkflowInstance();
        result.Data = JsonConvert.DeserializeObject(instance.Data, SerializerSettings);
        result.Description = instance.Description;
        result.Reference = instance.Reference;
        result.Id = instance.InstanceId.ToString();
        result.NextExecution = instance.NextExecution;
        result.Version = instance.Version;
        result.WorkflowDefinitionId = instance.WorkflowDefinitionId;
        result.Status = (WorkflowStatus)instance.Status;
        result.CreateTime = DateTime.SpecifyKind(instance.CreateTime, clock.Kind);
        if (instance.CompleteTime.HasValue)
            result.CompleteTime = DateTime.SpecifyKind(instance.CompleteTime.Value, clock.Kind);

        result.ExecutionPointers = new ExecutionPointerCollection(instance.ExecutionPointers.Count + 8);

        foreach (var ep in instance.ExecutionPointers)
        {
            var pointer = new ExecutionPointer();

            pointer.Id = ep.Id;
            pointer.StepId = ep.StepId;
            pointer.Active = ep.Active;

            if (ep.SleepUntil.HasValue)
                pointer.SleepUntil = DateTime.SpecifyKind(ep.SleepUntil.Value, clock.Kind);

            pointer.PersistenceData = JsonConvert.DeserializeObject(ep.PersistenceData ?? string.Empty, SerializerSettings);

            if (ep.StartTime.HasValue)
                pointer.StartTime = DateTime.SpecifyKind(ep.StartTime.Value, clock.Kind);

            if (ep.EndTime.HasValue)
                pointer.EndTime = DateTime.SpecifyKind(ep.EndTime.Value, clock.Kind);

            pointer.StepName = ep.StepName;

            pointer.RetryCount = ep.RetryCount;
            pointer.PredecessorId = ep.PredecessorId;
            pointer.ContextItem = JsonConvert.DeserializeObject(ep.ContextItem ?? string.Empty, SerializerSettings);

            if (!string.IsNullOrEmpty(ep.Children))
                pointer.Children = ep.Children.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries).ToList();

            pointer.EventName = ep.EventName;
            pointer.EventKey = ep.EventKey;
            pointer.EventPublished = ep.EventPublished;
            pointer.EventData = JsonConvert.DeserializeObject(ep.EventData ?? string.Empty, SerializerSettings);
            pointer.Outcome = JsonConvert.DeserializeObject(ep.Outcome ?? string.Empty, SerializerSettings);
            pointer.Status = (PointerStatus)ep.Status;

            if (!string.IsNullOrEmpty(ep.Scope))
                pointer.Scope = new List<string>(ep.Scope.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries));

            foreach (var attr in ep.ExtensionAttributes)
            {
                pointer.ExtensionAttributes[attr.AttributeKey] = JsonConvert.DeserializeObject(attr.AttributeValue, SerializerSettings);
            }

            result.ExecutionPointers.Add(pointer);
        }

        return result;
    }

    internal static EventSubscription ToEventSubscription(this WfSubscriptionEntity instance, IClock clock)
    {
        EventSubscription result = new EventSubscription();
        result.Id = instance.SubscriptionId.ToString();
        result.EventKey = instance.EventKey;
        result.EventName = instance.EventName;
        result.StepId = instance.StepId;
        result.ExecutionPointerId = instance.ExecutionPointerId;
        result.WorkflowId = instance.WorkflowId;
        result.SubscribeAsOf = DateTime.SpecifyKind(instance.SubscribeAsOf, clock.Kind);
        result.SubscriptionData = JsonConvert.DeserializeObject(instance.SubscriptionData, SerializerSettings);
        result.ExternalToken = instance.ExternalToken;
        result.ExternalTokenExpiry = instance.ExternalTokenExpiry;
        result.ExternalWorkerId = instance.ExternalWorkerId;

        return result;
    }

    internal static Event ToEvent(this WfEventEntity instance, IClock clock)
    {
        Event result = new Event();
        result.Id = instance.EventId.ToString();
        result.EventKey = instance.EventKey;
        result.EventName = instance.EventName;
        result.EventTime = DateTime.SpecifyKind(instance.EventTime, clock.Kind);
        result.IsProcessed = instance.IsProcessed;
        result.EventData = JsonConvert.DeserializeObject(instance.EventData, SerializerSettings);

        return result;
    }

    internal static ScheduledCommand ToScheduledCommand(this WfScheduledCommandEntity instance)
    {
        var result = new ScheduledCommand();
        result.CommandName = instance.CommandName;
        result.Data = instance.Data;
        result.ExecuteTime = instance.ExecuteTime;
        return result;
    }

    internal static DateTime ToUnfiyDateTime(this DateTime dateTime, IClock clock)
    {
        return clock.Kind switch
        {
            DateTimeKind.Utc => dateTime.ToUniversalTime(),
            DateTimeKind.Local => dateTime.ToLocalTime(),
            DateTimeKind.Unspecified => dateTime,
            _ => dateTime,
        };
    }

    internal static void AutoRegisterStepBodys(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();
        assembly.GetTypes().Where(x => x.IsSubclassOf(typeof(StepBody)) || x.IsSubclassOf(typeof(StepBodyAsync))).ToList().ForEach(x =>
        {
            services.AddTransient(x);
        });
    }
}
