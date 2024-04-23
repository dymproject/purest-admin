// MIT 许可证
// 版权 © 2023-present https://github.com/dymproject/purest-admin作者 以及贡献者
// 作者或版权持有人都不对任何索赔、损害或其他责任负责，无论这些追责来自合同、侵权或其它行为中，
// 还是产生于、源于或有关于本软件以及本软件的使用或其它处置。

namespace PurestAdmin.Application.NoticeServices.Dtos;
public class AddNoticeInput
{
    /// <summary>
    /// 标题
    /// </summary>
    [Required(ErrorMessage = "标题不能为空"), MaxLength(40, ErrorMessage = "不能超过40个字符或者20个汉字")]
    public string Title { get; set; }
    /// <summary>
    /// 内容
    /// </summary>
    [Required(ErrorMessage = "标题不能为空")]
    public string Content { get; set; }
    /// <summary>
    /// 类型
    /// </summary>
    public long NoticeType { get; set; }
    /// <summary>
    /// 级别
    /// </summary>
    public long Level { get; set; }
    /// <summary>
    /// 备注
    /// </summary>
    public string Remark { get; set; }
}
