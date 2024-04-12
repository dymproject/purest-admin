// MIT 许可证
// 版权 © 2023-present https://github.com/dymproject/purest-admin作者 以及贡献者
// 作者或版权持有人都不对任何索赔、损害或其他责任负责，无论这些追责来自合同、侵权或其它行为中，
// 还是产生于、源于或有关于本软件以及本软件的使用或其它处置。

namespace PurestAdmin.SqlSugar;
public class Repository<T> : SimpleClient<T> where T : class, new()
{
    public Repository(ISqlSugarClient db)
    {
        base.Context = db;
    }

    ///// <summary>
    ///// 扩展方法，自带方法不能满足的时候可以添加新方法
    ///// </summary>
    ///// <returns></returns>
    //public List<T> CommQuery(string json)
    //{
    //    //base.Context.Queryable<T>().ToList();可以拿到SqlSugarClient 做复杂操作
    //    return null;
    //}

}
