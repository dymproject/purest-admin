// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

using System.Globalization;
using System.Text;

using SqlSugar;

using Volo.Abp.DependencyInjection;

namespace PurestAdmin.Zero;
public class AutoEntity : ISingletonDependency
{
    private readonly ISqlSugarClient _db;
    private readonly AutoService _autoService;
    public AutoEntity(ISqlSugarClient db, AutoService autoService)
    {
        _db = db;
        _autoService = autoService;
    }

    private static readonly string[] sourceArray = ["string", "byte[]"];

    public void Initialization()
    {
        TextInfo ti = new CultureInfo("en-US", false).TextInfo;
        Console.WriteLine("请选择数据表");
        var tables = _db.DbMaintenance.GetTableInfoList(false);
        for (int i = 0; i < tables.Count; i++)
        {
            Console.WriteLine($"{i}\t{tables[i].Name}");
        }
        var replay = Console.ReadLine() ?? "0";

        var table = tables[int.Parse(replay)];
        Console.WriteLine($"您选择的表是：{table.Name}，请输入类名,如果直接回车则使用“默认类名”");
        var className = Console.ReadLine();
        if (className.IsNullOrEmpty())
        {
            var nameList = table.Name.Split('_').ToList();
            if (nameList.Count > 1)
            {
                nameList.RemoveAt(0);
            }
            className = nameList.Aggregate("", (current, fName) => current + ti.ToTitleCase(fName.ToLower()));
        }
        Console.WriteLine($"您的类名为：{className}");
        //创建实体
        var basePath = AppDomain.CurrentDomain.BaseDirectory;
        //命名空间
        StringBuilder sb = new("namespace PurestAdmin.SqlSugar.Entity;\r\n");

        sb.Append($"\r\n/// <summary>");
        sb.Append($"\r\n/// {table.Description}");
        sb.Append($"\r\n/// </summary>");
        sb.Append($"\r\n[SugarTable(\"{table.Name.ToUpper()}\")]");
        sb.Append($"\r\npublic partial class {className}Entity");

        Console.WriteLine("是否选择集成BaseEntity(直接回车为“是”，任意字符为“否”)");
        var containBase = false;
        replay = Console.ReadLine();
        if (replay.IsNullOrEmpty())
        {
            containBase = true;
            sb.Append(" : BaseEntity");
        }
        sb.Append("\r\n{");
        var columns = _db.DbMaintenance.GetColumnInfosByTableName(table.Name, false);

        foreach (var columnInfo in columns)
        {
            string[] baseColumnName = ["ID", "CREATE_BY", "CREATE_TIME", "UPDATE_BY", "UPDATE_TIME", "REMARK"];
            if (containBase && baseColumnName.Contains(columnInfo.DbColumnName.ToUpper()))
            {
                //排除base
                continue;
            }
            sb.Append("\r\n\t/// <summary>");
            sb.Append("\r\n\t/// " + columnInfo.ColumnDescription);
            sb.Append("\r\n\t/// </summary>");
            sb.Append($"\r\n\t[SugarColumn(ColumnName = \"{columnInfo.DbColumnName}\")]");
            var fieldName = Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(columnInfo.DbColumnName.ToLower());

            var strType = Common.GetCsharpType(columnInfo);
            var isNull = (columnInfo.IsNullable && !sourceArray.Contains(strType)) ? "?" : "";

            if (columnInfo.DbColumnName.Contains('_'))
            {
                var listFieldName = columnInfo.DbColumnName.Split('_').ToList();
                fieldName = listFieldName.Aggregate("", (current, fName) => current + ti.ToTitleCase(fName.ToLower()));
            }
            sb.Append($"\r\n\tpublic {strType}{isNull} {fieldName} {{ get; set; }}");
        }

        sb.Append("\r\n}");

        var entityPath = Path.Combine(basePath, "Entity");
        Directory.CreateDirectory(entityPath);
        var fullPath = Path.Combine(entityPath, $"{className}Entity.cs");
        using StreamWriter sw = new(fullPath);
        sw.Write(sb.ToString());
        Console.WriteLine($"实体已生成，完整文件路径为：{fullPath}");
        Console.WriteLine($"是否继续生成Service，直接回车则继续，输入任意字符则返回上级菜单");
        replay = Console.ReadLine();
        if (replay.IsNullOrEmpty())
        {
            _autoService.CreateService(table, className);
        }
    }
}
