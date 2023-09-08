// MIT 许可证
// 版权 © 2023-present dym 和所有贡献者
// 作者或版权持有人都不对任何索赔、损害或其他责任负责，无论这些追责来自合同、侵权或其它行为中，
// 还是产生于、源于或有关于本软件以及本软件的使用或其它处置。

using Newtonsoft.Json;

using PurestAdmin.Application.Interface.Dtos;

namespace PurestAdmin.Application.Interface.Services;
/// <summary>
/// 接口功能实现
/// </summary>
public class InterfaceService : IInterfaceService, ITransient
{
    /// <summary>
    /// db
    /// </summary>
    private readonly ISqlSugarClient _db;
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="db"></param>
    public InterfaceService(ISqlSugarClient db)
    {
        _db = db;
    }

    /// <summary>
    /// 分页查询
    /// </summary>
    public async Task<PagedList<InterfaceGroupProfile>> GetPagedListAsync(GetPagedListInput input)
    {
        if (!input.Path.IsNullOrEmpty())
        {
            var list = await _db.Queryable<InterfaceEntity>().Where(x => x.Path.Contains(input.Path)).ToListAsync();
            var pagedList = await _db.Queryable<InterfaceGroupEntity>().Where(x => list.Select(o => o.GroupId).Contains(x.Id)).ToPurestPagedListAsync(input.PageIndex, input.PageSize);
            var result = pagedList.Adapt<PagedList<InterfaceGroupProfile>>();
            result.Items.ToList().ForEach(x =>
            {
                x.Interfaces = list.Where(l => l.GroupId == x.Id).ToList().Adapt<List<InterfaceProfile>>();
            });
            return result;
        }
        var groups = await _db.Queryable<InterfaceGroupEntity>()
            .WhereIF(!input.GroupName.IsNullOrEmpty(), x => x.Name.Contains(input.GroupName))
            .Includes(x => x.Interfaces).ToPurestPagedListAsync(input.PageIndex, input.PageSize);
        return groups.Adapt<PagedList<InterfaceGroupProfile>>();
    }

    /// <summary>
    /// 导入接口文件
    /// </summary>
    /// <param name="file"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<int> ImportInterfaceAsync(IFormFile file)
    {
        StreamReader sr = new(file.OpenReadStream());
        string json = sr.ReadToEnd();
        var swaggerModel = JsonConvert.DeserializeObject<SwaggerModel>(json);
        await _db.Deleteable<InterfaceEntity>().ExecuteCommandAsync();
        foreach (var tag in swaggerModel.Tags)
        {
            var group = new InterfaceGroupEntity { Name = tag.Description, Code = tag.Name };
            var groupId = await _db.Insertable(group).ExecuteReturnSnowflakeIdAsync();
            var interfaceDetails = new List<InterfaceEntity>();
            foreach (var path in swaggerModel.Paths)
            {
                foreach (var pathItem in path.Value)
                {
                    if (pathItem.Value.Tags.Contains(tag.Name))
                    {
                        interfaceDetails.Add(new InterfaceEntity { GroupId = groupId, Path = path.Key, RequestMethod = pathItem.Key, Name = pathItem.Value.Summary });
                    }
                }
            }
            _ = await _db.Insertable(interfaceDetails).ExecuteReturnSnowflakeIdListAsync();
        }
        return swaggerModel.Tags.Length;
    }
}
