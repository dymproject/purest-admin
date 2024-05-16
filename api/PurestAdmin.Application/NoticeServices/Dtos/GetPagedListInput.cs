// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

namespace PurestAdmin.Application.NoticeServices.Dtos;
public class GetPagedListInput : PaginationParams
{
    /// <summary>
    /// 标题
    /// </summary>
    public string Title { get; set; }
    /// <summary>
    /// 类型
    /// </summary>
    public long? NoticeType { get; set; }
    /// <summary>
    /// 级别
    /// </summary>
    public long? Level { get; set; }
}
