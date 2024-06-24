// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

using Volo.Abp.Timing;

using WorkflowCore.Interface;

namespace PurestAdmin.Workflow;
public class AdminDateTimeProvider(IClock clock) : IDateTimeProvider
{
    private readonly IClock _clock = clock;

    public DateTime Now => _clock.Now;

    public DateTime UtcNow => _clock.Now;
}
