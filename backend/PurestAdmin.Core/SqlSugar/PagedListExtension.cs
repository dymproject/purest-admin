// MIT 许可证
// 版权 © 2023-present dym 和所有贡献者
// 作者或版权持有人都不对任何索赔、损害或其他责任负责，无论这些追责来自合同、侵权或其它行为中，
// 还是产生于、源于或有关于本软件以及本软件的使用或其它处置。

namespace PurestAdmin.Core;

/// <summary>
/// 分页泛型集合
/// </summary>
/// <typeparam name="T"></typeparam>
public class PagedList<T>
{
    /// <summary>
    /// 页码
    /// </summary>
    public int PageIndex { get; set; }

    /// <summary>
    /// 页容量
    /// </summary>
    public int PageSize { get; set; }

    /// <summary>
    /// 总条数
    /// </summary>
    public int Total { get; set; }

    /// <summary>
    /// 总页数
    /// </summary>
    public int PageCount { get; set; }

    /// <summary>
    /// 当前页集合
    /// </summary>
    public IEnumerable<T> Items { get; set; }
}

public static class PagedListExtension
{
    /// <summary>
    /// 分页拓展
    /// </summary>
    /// <param name="queryable"></param>
    /// <param name="pageIndex"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    public static async Task<PagedList<TResult>> ToPurestPagedListAsync<TResult>(this ISugarQueryable<TResult> queryable, int pageIndex, int pageSize)
    {
        RefAsync<int> total = 0;
        var items = await queryable.ToPageListAsync(pageIndex, pageSize, total);
        var totalPages = (int)Math.Ceiling(total / (double)pageSize);
        return new PagedList<TResult>
        {
            PageIndex = pageIndex,
            PageSize = pageSize,
            Items = items,
            Total = total.Value,
            PageCount = totalPages,
        };
    }
    /// <summary>
    /// 分页拓展
    /// </summary>
    /// <param name="list"></param>
    /// <param name="pageIndex"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    public static PagedList<TResult> ToPurestPagedList<TResult>(this IList<TResult> list, int pageIndex, int pageSize)
    {
        if (list == null)
        {
            return new PagedList<TResult>
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                Items = null,
                Total = 0,
                PageCount = 0,
            };
        }
        int total = list.Count;
        var items = list?.Skip(pageSize * (pageIndex - 1)).Take(pageSize);
        var totalPages = (int)Math.Ceiling(total / (double)pageSize);
        return new PagedList<TResult>
        {
            PageIndex = pageIndex,
            PageSize = pageSize,
            Items = items,
            Total = total,
            PageCount = totalPages,
        };
    }
}