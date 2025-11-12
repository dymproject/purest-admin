// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

using Mapster;

using PurestAdmin.SqlSugar;
using PurestAdmin.SqlSugar.Entity;

using Volo.Abp.BackgroundJobs;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Timing;

namespace PurestAdmin.BackgroundService;
public class BackgroundJobStore(IClock clock, Repository<BackgroundJobRecordEntity> repository) : IBackgroundJobStore, ITransientDependency
{
    private readonly IClock _clock = clock;
    private readonly Repository<BackgroundJobRecordEntity> _repository = repository;
    public async Task DeleteAsync(Guid jobId)
    {
        await _repository.DeleteByIdAsync(jobId);
    }

    public async Task<BackgroundJobInfo> FindAsync(Guid jobId)
    {
        var entity = await _repository.GetByIdAsync(jobId);
        return entity.Adapt<BackgroundJobInfo>();
    }

    public async Task<List<BackgroundJobInfo>> GetWaitingJobsAsync(string? applicationName, int maxResultCount)
    {
        var pagedList = await _repository.AsQueryable()
            .WhereIF(!applicationName.IsNullOrEmpty(), t => t.ApplicationName == applicationName)
            .Where(t => !t.IsAbandoned && t.NextTryTime <= _clock.Now)
            .OrderByDescending(t => t.Priority)
            .OrderBy(t => t.TryCount)
            .OrderBy(t => t.NextTryTime)
            .ToPurestPagedListAsync(1, maxResultCount);
        var result = pagedList.Items.Adapt<List<BackgroundJobInfo>>();
        return pagedList.Items.Adapt<List<BackgroundJobInfo>>();
    }

    public async Task InsertAsync(BackgroundJobInfo jobInfo)
    {
        var entity = jobInfo.Adapt<BackgroundJobRecordEntity>();
        await _repository.InsertAsync(entity);
    }

    public async Task UpdateAsync(BackgroundJobInfo jobInfo)
    {
        var entity = await _repository.GetByIdAsync(jobInfo.Id);
        entity = jobInfo.Adapt(entity);
        await _repository.UpdateAsync(entity);
    }
}
