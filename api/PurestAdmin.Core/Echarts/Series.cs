// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

namespace PurestAdmin.Core.Echarts;
public class Series
{
    public Series() { }
    public Series(string name, string type)
    {
        Name = name;
        Type = type;
    }
    public string Name { get; set; }
    public List<decimal> Data { get; set; } = [];
    public string Type { get; set; }
}
