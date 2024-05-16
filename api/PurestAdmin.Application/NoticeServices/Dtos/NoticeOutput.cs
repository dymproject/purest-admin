// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

namespace PurestAdmin.Application.NoticeServices.Dtos;
public class NoticeOutput
{
    /// <summary>
    /// Id
    /// </summary>
    public long Id { get; set; }
    /// <summary>
    /// 标题
    /// </summary>
    public string Title { get; set; }
    /// <summary>
    /// 内容
    /// </summary>
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
    /// <summary>
    /// 类型string
    /// </summary>
    public string NoticeTypeString { get; set; }
    /// <summary>
    /// 级别string
    /// </summary>
    public string LevelString { get; set; }
}
