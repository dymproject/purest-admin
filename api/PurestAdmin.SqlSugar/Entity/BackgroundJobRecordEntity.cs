// MIT 许可证
// 版权 © 2023-present https://github.com/dymproject/purest-admin作者 以及贡献者
// 作者或版权持有人都不对任何索赔、损害或其他责任负责，无论这些追责来自合同、侵权或其它行为中，
// 还是产生于、源于或有关于本软件以及本软件的使用或其它处置。

namespace PurestAdmin.SqlSugar.Entity;
/// <summary>
/// 后台作业记录
/// </summary>
/// <remarks>详情参照Volo.Abp.BackgroundJobs;</remarks>
[SugarTable("PUREST_BACKGROUND_JOB_RECORD")]
public class BackgroundJobRecordEntity
{
    /// <summary>
    /// Id
    ///</summary>
    [SugarColumn(ColumnName = "ID", IsPrimaryKey = true)]
    public Guid Id { get; set; }
    /// <summary>
    /// Type of the job.
    /// It's AssemblyQualifiedName of job type.
    /// </summary>
    [SugarColumn(ColumnName = "JOB_NAME")]
    public string JobName { get; set; }

    /// <summary>
    /// Job arguments as serialized string.
    /// </summary>
    [SugarColumn(ColumnName = "JOB_ARGS")]
    public string JobArgs { get; set; }

    /// <summary>
    /// Try count of this job.
    /// A job is re-tried if it fails.
    /// </summary>
    [SugarColumn(ColumnName = "TRY_COUNT")]
    public short TryCount { get; set; }

    /// <summary>
    /// Creation time of this job.
    /// </summary>
    [SugarColumn(ColumnName = "CREATION_TIME")]
    public DateTime CreationTime { get; set; }

    /// <summary>
    /// Next try time of this job.
    /// </summary>
    [SugarColumn(ColumnName = "NEXT_TRY_TIME")]
    public DateTime NextTryTime { get; set; }

    /// <summary>
    /// Last try time of this job.
    /// </summary>
    [SugarColumn(ColumnName = "LAST_TRY_TIME")]
    public DateTime? LastTryTime { get; set; }

    /// <summary>
    /// This is true if this job is continuously failed and will not be executed again.
    /// </summary>
    [SugarColumn(ColumnName = "IS_ABANDONED")]
    public bool IsAbandoned { get; set; }

    /// <summary>
    /// Priority of this job.
    /// </summary>
    [SugarColumn(ColumnName = "PRIORITY")]
    public int Priority { get; set; } = 15;
}
