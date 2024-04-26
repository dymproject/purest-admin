// MIT 许可证
// 版权 © 2023-present https://github.com/dymproject/purest-admin作者 以及贡献者
// 作者或版权持有人都不对任何索赔、损害或其他责任负责，无论这些追责来自合同、侵权或其它行为中，
// 还是产生于、源于或有关于本软件以及本软件的使用或其它处置。

using Mapster;

using PurestAdmin.SqlSugar;
using PurestAdmin.SqlSugar.Entity;

using Volo.Abp.BackgroundJobs;
using Volo.Abp.DependencyInjection;

namespace PurestAdmin.Multiplex;
public class BackgroundJobStore(Repository<BackgroundJobRecordEntity> repository) : IBackgroundJobStore, ITransientDependency
{
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

    public async Task<List<BackgroundJobInfo>> GetWaitingJobsAsync(int maxResultCount)
    {
        var pagedList = await _repository.AsQueryable()
            .Where(t => !t.IsAbandoned && t.NextTryTime <= DateTime.Now)
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
