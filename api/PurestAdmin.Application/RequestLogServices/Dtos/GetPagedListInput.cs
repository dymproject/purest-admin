// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

namespace PurestAdmin.Application.RequestLogServices.Dtos;
public class GetPagedListInput : PaginationParams
{
    /// <summary>
    /// 日期
    /// </summary>
    [Required(ErrorMessage = "日期不能为空")]
    public DateTime RequestDate { get; set; }
    /// <summary>
    /// 控制器名称
    /// </summary>
    public string ControllerName { get; set; }
    /// <summary>
    /// 方法名
    /// </summary>
    public string ActionName { get; set; }
}
