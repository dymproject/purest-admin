// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

using Mapster;

using Microsoft.EntityFrameworkCore;

using PurestAdmin.SqlSugar;
using PurestAdmin.SqlSugar.Entity;

using SqlSugar;

using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace PurestAdmin.Workflow;
public class AdminPersistenceProvider(ISqlSugarClient db) : IPersistenceProvider, ISingletonDependency
{
    private readonly ISqlSugarClient _db = db;

    public bool SupportsScheduledCommands => true;
    public async Task<string> CreateEventSubscription(EventSubscription subscription, CancellationToken cancellationToken = default)
    {
        subscription.Id = Guid.NewGuid().ToString();
        var subscriptionEntity = subscription.ToPersistable();
        await _db.Insertable(subscriptionEntity).ExecuteReturnSnowflakeIdAsync(cancellationToken);
        return subscription.Id;
    }

    public async Task<string> CreateNewWorkflow(WorkflowInstance workflow, CancellationToken cancellationToken = default)
    {
        workflow.Id = Guid.NewGuid().ToString();
        var workflowEntity = workflow.ToPersistable();
        await _db.InsertNav(workflowEntity).Include(x => x.ExecutionPointers).ThenInclude(x => x.ExtensionAttributes).ExecuteCommandAsync();
        return workflow.Id;
    }

    public async Task<IEnumerable<string>> GetRunnableInstances(DateTime asAt, CancellationToken cancellationToken = default)
    {
        var now = asAt.Ticks;
        var raw = await _db.Queryable<WfWorkflowEntity>()
            .Where(x => x.NextExecution.HasValue && (x.NextExecution <= now) && (x.Status == (int)WorkflowStatus.Runnable))
            .Select(x => x.InstanceId)
            .ToListAsync(cancellationToken);
        return raw.Select(s => s.ToString()).ToList();
    }

    public async Task<IEnumerable<WorkflowInstance>> GetWorkflowInstances(WorkflowStatus? status, string type, DateTime? createdFrom, DateTime? createdTo, int skip, int take)
    {
        var query = _db.Queryable<WfWorkflowEntity>().Includes(x => x.ExecutionPointers, p => p.ExtensionAttributes);
        if (status.HasValue)
            query = query.Where(x => x.Status == (int)status.Value);

        if (!String.IsNullOrEmpty(type))
            query = query.Where(x => x.WorkflowDefinitionId == type);

        if (createdFrom.HasValue)
            query = query.Where(x => x.CreateTime >= createdFrom.Value);

        if (createdTo.HasValue)
            query = query.Where(x => x.CreateTime <= createdTo.Value);

        var pagedList = await query.ToPurestPagedListAsync(skip, take);
        List<WorkflowInstance> result = [];
        pagedList.Items?.ForEach(x =>
        {
            result.Add(x.ToWorkflowInstance());
        });
        return result;
    }

    public async Task<WorkflowInstance> GetWorkflowInstance(string Id, CancellationToken cancellationToken = default)
    {
        var query = _db.Queryable<WfWorkflowEntity>().Includes(x => x.ExecutionPointers, p => p.ExtensionAttributes);
        var raw = await query.FirstAsync(x => x.InstanceId == Id, cancellationToken);
        return raw.ToWorkflowInstance();
    }

    public async Task<IEnumerable<WorkflowInstance>> GetWorkflowInstances(IEnumerable<string> ids, CancellationToken cancellationToken = default)
    {
        if (ids == null)
        {
            return [];
        }
        var query = _db.Queryable<WfWorkflowEntity>().Includes(x => x.ExecutionPointers, p => p.ExtensionAttributes);
        var list = await query.Where(x => ids.Contains(x.InstanceId)).ToListAsync(cancellationToken);
        return list.Select(x => x.ToWorkflowInstance());
    }

    public async Task PersistWorkflow(WorkflowInstance workflow, CancellationToken cancellationToken = default)
    {
        var query = _db.Queryable<WfWorkflowEntity>().Includes(x => x.ExecutionPointers, p => p.ExtensionAttributes);
        var existingEntity = await query.FirstAsync(x => x.InstanceId == workflow.Id, cancellationToken);
        var persistable = workflow.ToPersistable(existingEntity);
    }

    public async Task PersistWorkflow(WorkflowInstance workflow, List<EventSubscription> subscriptions, CancellationToken cancellationToken = default)
    {
        var query = _db.Queryable<WfWorkflowEntity>().Includes(x => x.ExecutionPointers, p => p.ExtensionAttributes);
        var existingEntity = await query.FirstAsync(x => x.InstanceId == workflow.Id, cancellationToken);
        var workflowPersistable = workflow.ToPersistable(existingEntity);
        List<WfSubscriptionEntity> wfSubscriptions = [];
        foreach (var subscription in subscriptions)
        {
            subscription.Id = Guid.NewGuid().ToString();
            var subscriptionPersistable = subscription.ToPersistable();
            wfSubscriptions.Add(subscriptionPersistable);
        }
        await _db.Insertable(wfSubscriptions).ExecuteReturnSnowflakeIdListAsync(cancellationToken);
    }

    public async Task TerminateSubscription(string eventSubscriptionId, CancellationToken cancellationToken = default)
    {
        var existing = await _db.Queryable<WfSubscriptionEntity>().FirstAsync(x => x.SubscriptionId == eventSubscriptionId, cancellationToken);
        if (existing != null)
        {
            await _db.Deleteable(existing).ExecuteCommandAsync(cancellationToken);
        }
    }

    public virtual void EnsureStoreExists()
    {
        //using (var context = ConstructDbContext())
        //{
        //    if (_canCreateDB && !_canMigrateDB)
        //    {
        //        context.Database.EnsureCreated();
        //        return;
        //    }

        //    if (_canMigrateDB)
        //    {
        //        context.Database.Migrate();
        //        return;
        //    }
        //}
    }

    public async Task<IEnumerable<EventSubscription>> GetSubscriptions(string eventName, string eventKey, DateTime asOf, CancellationToken cancellationToken = default)
    {
        //asOf = asOf.ToUniversalTime();
        var raw = await _db.Queryable<WfSubscriptionEntity>()
            .Where(x => x.EventName == eventName && x.EventKey == eventKey && x.SubscribeAsOf <= asOf)
            .ToListAsync(cancellationToken);

        return raw.Select(item => item.ToEventSubscription()).ToList();
    }

    public async Task<string> CreateEvent(Event newEvent, CancellationToken cancellationToken = default)
    {
        newEvent.Id = Guid.NewGuid().ToString();
        var persistable = newEvent.ToPersistable();
        await _db.Insertable(persistable).ExecuteReturnSnowflakeIdAsync(cancellationToken);
        return newEvent.Id;
    }

    public async Task<Event> GetEvent(string id, CancellationToken cancellationToken = default)
    {
        var raw = await _db.Queryable<WfEventEntity>().FirstAsync(x => x.EventId == id, cancellationToken);
        if (raw == null) return null;
        return raw.ToEvent();
    }

    public async Task<IEnumerable<string>> GetRunnableEvents(DateTime asAt, CancellationToken cancellationToken = default)
    {
        //var now = asAt.ToUniversalTime();
        var raw = await _db.Queryable<WfEventEntity>()
              .Where(x => !x.IsProcessed)
              .Where(x => x.EventTime <= asAt)
              .Select(x => x.EventId)
              .ToListAsync(cancellationToken);

        return raw.Select(s => s.ToString()).ToList();
    }

    public async Task MarkEventProcessed(string id, CancellationToken cancellationToken = default)
    {
        await _db.Updateable<WfEventEntity>()
            .SetColumns(x => x.IsProcessed == true)
            .Where(x => x.EventId == id)
            .ExecuteCommandAsync(cancellationToken);
    }

    public async Task<IEnumerable<string>> GetEvents(string eventName, string eventKey, DateTime asOf, CancellationToken cancellationToken)
    {
        var raw = await _db.Queryable<WfEventEntity>()
                .Where(x => x.EventName == eventName && x.EventKey == eventKey)
                .Where(x => x.EventTime >= asOf)
                .Select(x => x.EventId)
                .ToListAsync(cancellationToken);
        return raw;
    }

    public async Task MarkEventUnprocessed(string id, CancellationToken cancellationToken = default)
    {
        await _db.Updateable<WfEventEntity>()
            .SetColumns(x => x.IsProcessed == false)
            .Where(x => x.EventId == id)
            .ExecuteCommandAsync(cancellationToken);
    }

    public async Task PersistErrors(IEnumerable<ExecutionError> errors, CancellationToken cancellationToken = default)
    {
        var executionErrors = errors as ExecutionError[] ?? errors.ToArray();
        List<WfErrorEntity> wfErrorEntities = [];
        if (executionErrors.Length != 0)
        {
            foreach (var error in executionErrors)
            {
                wfErrorEntities.Add(error.ToPersistable());
            }
            await _db.Insertable(wfErrorEntities).ExecuteReturnSnowflakeIdListAsync(cancellationToken);
        }
    }

    public async Task<EventSubscription> GetSubscription(string eventSubscriptionId, CancellationToken cancellationToken = default)
    {
        var raw = await _db.Queryable<WfSubscriptionEntity>().FirstAsync(x => x.SubscriptionId == eventSubscriptionId, cancellationToken);
        return raw.ToEventSubscription();
    }

    public async Task<EventSubscription> GetFirstOpenSubscription(string eventName, string eventKey, DateTime asOf, CancellationToken cancellationToken = default)
    {
        var raw = await _db.Queryable<WfSubscriptionEntity>().FirstAsync(x => x.EventName == eventName && x.EventKey == eventKey && x.SubscribeAsOf <= asOf && x.ExternalToken == null, cancellationToken);
        return raw.ToEventSubscription();
    }

    public async Task<bool> SetSubscriptionToken(string eventSubscriptionId, string token, string workerId, DateTime expiry, CancellationToken cancellationToken = default)
    {
        var existingEntity = await _db.Queryable<WfSubscriptionEntity>()
            .Where(x => x.SubscriptionId == eventSubscriptionId)
            .FirstAsync(cancellationToken);

        existingEntity.ExternalToken = token;
        existingEntity.ExternalWorkerId = workerId;
        existingEntity.ExternalTokenExpiry = expiry;
        await _db.Updateable(existingEntity).ExecuteCommandAsync(cancellationToken);
        return true;
    }

    public async Task ClearSubscriptionToken(string eventSubscriptionId, string token, CancellationToken cancellationToken = default)
    {
        var uid = new Guid(eventSubscriptionId);
        var existingEntity = await _db.Queryable<WfSubscriptionEntity>()
            .Where(x => x.SubscriptionId == eventSubscriptionId)
            .FirstAsync(cancellationToken);

        if (existingEntity.ExternalToken != token)
            throw new InvalidOperationException();

        existingEntity.ExternalToken = null;
        existingEntity.ExternalWorkerId = null;
        existingEntity.ExternalTokenExpiry = null;
        await _db.Updateable(existingEntity).ExecuteCommandAsync(cancellationToken);
    }

    public async Task ScheduleCommand(ScheduledCommand command)
    {
        try
        {
            var persistable = command.ToPersistable();
            await _db.Insertable(persistable).ExecuteReturnSnowflakeIdAsync();
        }
        catch (DbUpdateException)
        {
            //log
        }
    }

    public async Task ProcessCommands(DateTimeOffset asOf, Func<ScheduledCommand, Task> action, CancellationToken cancellationToken = default)
    {
        var cursor = await _db.Queryable<WfScheduledCommandEntity>()
                 .Where(x => x.ExecuteTime < asOf.Ticks)
                 .ToListAsync(cancellationToken);
        foreach (var command in cursor)
        {
            try
            {
                await action(command.ToScheduledCommand());
                await _db.Deleteable(command).ExecuteCommandAsync(cancellationToken);
            }
            catch (Exception)
            {
                //TODO: add logger
            }
        }
    }
}
