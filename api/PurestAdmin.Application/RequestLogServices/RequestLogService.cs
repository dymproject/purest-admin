// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

using PurestAdmin.Application.RequestLogServices.Dtos;
using PurestAdmin.Core.Echarts;

namespace PurestAdmin.Application.RequestLogServices;
/// <summary>
/// 请求日志服务
/// </summary>
public class RequestLogService(ISqlSugarClient client) : ApplicationService
{
    private readonly ISqlSugarClient _client = client;

    /// <summary>
    /// 分页查询
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task<PagedList<RequestLogOutput>> GetPagedListAsync(GetPagedListInput input)
    {
        var pagedList = await _client.Queryable<RequestLogEntity>()
            .WhereIF(!input.ControllerName.IsNullOrEmpty(), x => x.ControllerName.Contains(input.ControllerName))
            .WhereIF(!input.ActionName.IsNullOrEmpty(), x => x.ActionName.Contains(input.ActionName))
            .Where(x => x.CreateTime.ToString("yyyy-MM-dd") == input.RequestDate.ToString("yyyy-MM-dd"))
            .OrderByDescending(x => x.CreateTime)
            .ToPurestPagedListAsync(input.PageIndex, input.PageSize);
        return pagedList.Adapt<PagedList<RequestLogOutput>>();
    }
    /// <summary>
    /// 按日期获取请求日志统计数
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task<ChartModel> GetRequestLogChartAsync(GetRequestLogChartInput input)
    {
        var list = await _client.Queryable<RequestLogEntity>()
            .WhereIF(input.StartTime.HasValue, x => x.CreateTime >= input.StartTime)
            .WhereIF(input.EndTime.HasValue, x => x.CreateTime <= input.EndTime)
            .Select(x => new { FormatTime = x.CreateTime.Date, x.ClientIp })
            .MergeTable()
            .GroupBy(x => new { x.FormatTime, x.ClientIp })
            .Select(x => new
            {
                Ip = x.ClientIp,
                Count = SqlFunc.AggregateCount(x.ClientIp),
                RequestDate = x.FormatTime.ToString("yyyy-MM-dd")
            }).ToListAsync();

        var group = list.GroupBy(x => x.RequestDate).ToList();
        var x = new XAxis
        {
            Data = group.Select(x => x.Key).ToList(),
            Type = "category"
        };
        var legend = new Legend { Data = ["访问人数"] };
        var s1 = new Series("访问人数", "line");
        foreach (var item in x.Data)
        {
            var count = group.First(x => x.Key == item).Count();
            s1.Data.Add(count);
        }
        var result = new ChartModel
        {
            XAxis = x,
            Legend = legend,
            Series = [s1]
        };
        return result;
    }
}
