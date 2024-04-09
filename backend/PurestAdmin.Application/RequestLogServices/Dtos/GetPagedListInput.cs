// MIT 许可证
// 版权 © 2023-present https://github.com/dymproject/purest-admin作者 以及贡献者
// 作者或版权持有人都不对任何索赔、损害或其他责任负责，无论这些追责来自合同、侵权或其它行为中，
// 还是产生于、源于或有关于本软件以及本软件的使用或其它处置。

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    /// <summary>
    /// 是否成功
    /// </summary>
    public bool? IsSuccess { get; set; }
}
