// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

namespace PurestAdmin.Core.Echarts;
public class ChartModel
{
    public Legend Legend { get; set; }
    public XAxis XAxis { get; set; }
    public List<Series> Series { get; set; } = [];
}
