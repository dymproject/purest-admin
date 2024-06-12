// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

namespace PurestAdmin.SqlSugar;
public class ConnectionOption
{
    /// <summary>
    /// 数据类型(默认mysql)
    /// </summary>
    public DbType DbTypeEnum
    {
        get
        {
            return Enum.TryParse(DbType, true, out DbType result) ? result : global::SqlSugar.DbType.MySql;
        }
    }
    /// <summary>
    /// 连接串
    /// </summary>
    public string ConnectionString { get; set; }
    /// <summary>
    /// 自动关闭连接
    /// </summary>
    public bool IsAutoCloseConnection => true;
    /// <summary>
    /// 数据类型
    /// </summary>
    public string DbType { get; set; }
}
