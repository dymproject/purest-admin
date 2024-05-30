// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

using System.Globalization;

using SqlSugar;

using Volo.Abp.DependencyInjection;

namespace PurestAdmin.Zero;
public class AutoService(ISqlSugarClient db) : ISingletonDependency
{
    private readonly ISqlSugarClient _db = db;
    private static readonly string[] sourceArray = ["string", "byte[]"];
    public void Initialization()
    {
        Console.WriteLine("请选择数据表");
        var tables = _db.DbMaintenance.GetTableInfoList(false);
        for (int i = 0; i < tables.Count; i++)
        {
            Console.WriteLine($"{i}\t{tables[i].Name}");
        }
        var replay = Console.ReadLine() ?? "0";

        var table = tables[int.Parse(replay)];
        Console.WriteLine($"您选择的表是：{table.Name}，请输入类名,如果直接回车则使用默认类名");
        var className = Console.ReadLine();
        if (className.IsNullOrEmpty())
        {
            var nameList = table.Name.Split('_').ToList();
            if (nameList.Count > 1)
            {
                nameList.RemoveAt(0);
            }
            TextInfo ti = new CultureInfo("en-US", false).TextInfo;
            className = nameList.Aggregate("", (current, fName) => current + ti.ToTitleCase(fName.ToLower()));
        }
        Console.WriteLine($"您的类名为：{className}");
        CreateService(table, className);
    }

    public void CreateService(DbTableInfo table, string className)
    {
        Console.WriteLine($"请输入命名空间，如直接回车则为默认命名空间PurestAdmin.Application.{className}Services");
        var nameSpace = Console.ReadLine();
        if (nameSpace.IsNullOrEmpty())
        {
            nameSpace = $"PurestAdmin.Application.{className}Services";
        }
        Console.WriteLine($"您的命名空间为：{nameSpace}");

        var columns = _db.DbMaintenance.GetColumnInfosByTableName(table.Name, false);
        var outputProps = Common.GetOutputDtoString(columns);
        var inputProps = Common.GetInputDtoString(columns);


        var basePath = AppDomain.CurrentDomain.BaseDirectory;
        var servicePath = Path.Combine(basePath, "Services", $"{className}Services");
        var dtoPath = Path.Combine(servicePath, "Dtos");
        Directory.CreateDirectory(dtoPath);

        //生成GetPagedListInput
        using (var reader = new StreamReader(Path.Combine(basePath, "Templates", "GetPagedListInputTemplate.txt")))
        {
            var content = reader.ReadToEnd();
            var result = content.Replace("@NameSpace@", nameSpace).Replace("@ClassName@", className);
            using StreamWriter writer = new(Path.Combine(dtoPath, $"GetPagedListInput.cs"));
            writer.Write(result);
        };
        Console.WriteLine($"生成GetPagedListInput");
        //生成AddInput
        using (var reader = new StreamReader(Path.Combine(basePath, "Templates", "AddTemplate.txt")))
        {
            var content = reader.ReadToEnd();
            var result = content.Replace("@NameSpace@", nameSpace).Replace("@ClassName@", className).Replace("@props@", inputProps);
            using StreamWriter writer = new(Path.Combine(dtoPath, $"Add{className}Input.cs"));
            writer.Write(result);
        };
        Console.WriteLine($"生成Add{className}Input.cs");
        //生成Output
        using (var reader = new StreamReader(Path.Combine(basePath, "Templates", "OutputTemplate.txt")))
        {
            var content = reader.ReadToEnd();
            var result = content.Replace("@NameSpace@", nameSpace).Replace("@ClassName@", className).Replace("@props@", outputProps);
            using StreamWriter writer = new(Path.Combine(dtoPath, $"{className}Output.cs"));
            writer.Write(result);
        };
        Console.WriteLine($"生成{className}Output.cs");
        //生成PutInput
        using (var reader = new StreamReader(Path.Combine(basePath, "Templates", "PutInputTemplate.txt")))
        {
            var content = reader.ReadToEnd();
            var result = content.Replace("@NameSpace@", nameSpace).Replace("@ClassName@", className);
            using StreamWriter writer = new(Path.Combine(dtoPath, $"Put{className}Input.cs"));
            writer.Write(result);
        };
        Console.WriteLine($"生成Put{className}Input.cs");
        //生成service
        using (var reader = new StreamReader(Path.Combine(basePath, "Templates", "ServiceTemplate.txt")))
        {
            var content = reader.ReadToEnd();
            var result = content.Replace("@NameSpace@", nameSpace).Replace("@ClassName@", className);
            using StreamWriter writer = new(Path.Combine(servicePath, $"{className}Service.cs"));
            writer.Write(result);
        };
        Console.WriteLine($"生成{className}Service.cs");
        Console.WriteLine($"Services生成成功，路径为{servicePath}");
    }
}
