// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

using System.Globalization;
using System.Text;

using SqlSugar;

namespace PurestAdmin.Zero;
public static class Common
{
    public static string GetCsharpType(DbColumnInfo columnInfo)
    {
        string strType;
        strType = columnInfo.DataType.ToLower() switch
        {
            "tinyint" => columnInfo.Length == 1 ? "bool" : "int",
            "smallint" or "mediumint" or "int" => "int",
            "bigint" => "long",
            "float" or "double" => "decimal",
            "decimal" => columnInfo.Scale > 0 ? "decimal" : (columnInfo.Length > 10 ? "long" : "int"),
            "char" or "varchar" or "text" or "tinytext" or "mediumtext" or "longtext" => "string",
            "blob" or "tinyblob" or "mediumblob" or "longblob" => "byte[]",
            "date" or "datetime" => "DateTime",
            _ => "string",
        };
        return strType;
    }
    private static readonly string[] sourceArray = ["string", "byte[]"];
    public static string GetInputDtoString(List<DbColumnInfo> dbColumns)
    {
        StringBuilder dtoResult = new();
        TextInfo ti = new CultureInfo("en-US", false).TextInfo;
        foreach (var columnInfo in dbColumns)
        {
            //排除
            string[] baseColumnName = ["ID", "CREATE_BY", "CREATE_TIME", "UPDATE_BY", "UPDATE_TIME"];
            if (baseColumnName.Contains(columnInfo.DbColumnName.ToUpper()))
            {
                continue;
            }
            dtoResult.Append("\r\n\t/// <summary>");
            dtoResult.Append("\r\n\t/// " + columnInfo.ColumnDescription);
            dtoResult.Append("\r\n\t/// </summary>");

            var strType = GetCsharpType(columnInfo);
            if (!columnInfo.IsNullable)
            {
                if (strType == "string")
                {
                    if (columnInfo.DataType.Contains("text"))
                    {
                        dtoResult.AppendFormat("\r\n\t[Required(ErrorMessage = \"{0}\")]", columnInfo.ColumnDescription + "不能为空");
                    }
                    else
                    {
                        dtoResult.AppendFormat("\r\n\t[Required(ErrorMessage = \"{0}\"), MaxLength({1}, ErrorMessage = \"{2}\")]", columnInfo.ColumnDescription + "不能为空", columnInfo.Length, columnInfo.ColumnDescription + "最大长度为：" + columnInfo.Length);
                    }

                }
                else
                {
                    dtoResult.AppendFormat("\r\n\t[Required(ErrorMessage = \"{0}\")]", columnInfo.ColumnDescription + "不能为空");
                }
            }

            var fieldName = Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(columnInfo.DbColumnName.ToLower());
            var isNull = (columnInfo.IsNullable && !sourceArray.Contains(strType)) ? "?" : "";

            if (columnInfo.DbColumnName.Contains('_'))
            {
                var listFieldName = columnInfo.DbColumnName.Split('_').ToList();
                fieldName = listFieldName.Aggregate("", (current, fName) => current + ti.ToTitleCase(fName.ToLower()));
            }

            dtoResult.Append($"\r\n\tpublic {strType}{isNull} {fieldName} {{ get; set; }}");
        }
        return dtoResult.ToString();
    }

    public static string GetOutputDtoString(List<DbColumnInfo> dbColumns)
    {
        StringBuilder dtoResult = new();
        TextInfo ti = new CultureInfo("en-US", false).TextInfo;
        foreach (var columnInfo in dbColumns)
        {
            //排除
            string[] baseColumnName = ["CREATE_BY", "CREATE_TIME", "UPDATE_BY", "UPDATE_TIME"];
            if (baseColumnName.Contains(columnInfo.DbColumnName.ToUpper()))
            {
                continue;
            }
            dtoResult.Append("\r\n\t/// <summary>");
            dtoResult.Append("\r\n\t/// " + columnInfo.ColumnDescription);
            dtoResult.Append("\r\n\t/// </summary>");

            var strType = GetCsharpType(columnInfo);
            var fieldName = Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(columnInfo.DbColumnName.ToLower());
            var isNull = (columnInfo.IsNullable && !sourceArray.Contains(strType)) ? "?" : "";

            if (columnInfo.DbColumnName.Contains('_'))
            {
                var listFieldName = columnInfo.DbColumnName.Split('_').ToList();
                fieldName = listFieldName.Aggregate("", (current, fName) => current + ti.ToTitleCase(fName.ToLower()));
            }

            dtoResult.Append($"\r\n\tpublic {strType}{isNull} {fieldName} {{ get; set; }}");
        }
        return dtoResult.ToString();
    }
}
