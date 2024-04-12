// MIT 许可证
// 版权 © 2023-present https://github.com/dymproject/purest-admin作者 以及贡献者
// 作者或版权持有人都不对任何索赔、损害或其他责任负责，无论这些追责来自合同、侵权或其它行为中，
// 还是产生于、源于或有关于本软件以及本软件的使用或其它处置。

namespace PurestAdmin.Application.RequestLogServices.Dtos;
public class RequestLogOutput
{
    /// <summary>
    /// Id
    ///</summary>
    public long Id { get; set; }
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
    /// <summary>
    /// 时间
    /// </summary>
    public DateTime CreateTime { get; set; }
}
