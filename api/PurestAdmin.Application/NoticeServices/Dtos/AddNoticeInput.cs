// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

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
