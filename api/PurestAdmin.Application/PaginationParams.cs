// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

namespace PurestAdmin.Application;
public class PaginationParams
{
    /// <summary>
    /// 页码
    /// </summary>
    [Range(1, int.MaxValue, ErrorMessage = "页码只能在 1 到 2147483647 之间")]
    public int PageIndex { get; set; } = 1;

    /// <summary>
    /// 页容量
    /// </summary>
    [Range(5, 200, ErrorMessage = "每页容量只能在 5 到 200 之间")]
    public int PageSize { get; set; } = 10;
}
