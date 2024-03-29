// MIT 许可证
// 版权 © 2023-present https://github.com/dymproject/purest-admin作者 以及贡献者
// 作者或版权持有人都不对任何索赔、损害或其他责任负责，无论这些追责来自合同、侵权或其它行为中，
// 还是产生于、源于或有关于本软件以及本软件的使用或其它处置。

namespace PurestAdmin.SqlSugar.Entity;

/// <summary>
/// 请求日志表
/// </summary>
[SugarTable("PUREST_REQUEST_LOG")]
public partial class RequestLogEntity : BaseEntity
{
    /// <summary>
    /// 控制器
    /// </summary>
    [SugarColumn(ColumnName = "CONTROLLER_NAME")]
    public string ControllerName { get; set; }
    /// <summary>
    /// 方法名
    /// </summary>
    [SugarColumn(ColumnName = "ACTION_NAME")]
    public string ActionName { get; set; }
    /// <summary>
    /// 请求类型
    /// </summary>
    [SugarColumn(ColumnName = "REQUEST_METHOD")]
    public string RequestMethod { get; set; }
    /// <summary>
    /// 服务器环境
    /// </summary>
    [SugarColumn(ColumnName = "ENVIRONMENT_NAME")]
    public string EnvironmentName { get; set; }
    /// <summary>
    /// 完成情况
    /// </summary>
    [SugarColumn(ColumnName = "IS_SUCCESS")]
    public bool IsSuccess { get; set; }
    /// <summary>
    /// 执行耗时
    /// </summary>
    [SugarColumn(ColumnName = "ELAPSED_TIME")]
    public long ElapsedTime { get; set; }
    /// <summary>
    /// 客户端IP
    /// </summary>
    [SugarColumn(ColumnName = "CLIENT_IP")]
    public string ClientIp { get; set; }
}