// MIT 许可证
// 版权 © 2023-present https://github.com/dymproject/purest-admin作者 以及贡献者
// 作者或版权持有人都不对任何索赔、损害或其他责任负责，无论这些追责来自合同、侵权或其它行为中，
// 还是产生于、源于或有关于本软件以及本软件的使用或其它处置。

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Newtonsoft.Json;

using PurestAdmin.Application.InterfaceServices.Dtos;
using PurestAdmin.Core.Attributes;

namespace PurestAdmin.Application.InterfaceServices;
/// <summary>
/// 接口功能实现
/// </summary>
public class InterfaceService(ISqlSugarClient db) : ApplicationService
{
    /// <summary>
    /// db
    /// </summary>
    private readonly ISqlSugarClient _db = db;

    /// <summary>
    /// 分页查询
    /// </summary>
    public async Task<PagedList<InterfaceGroupOutput>> GetPagedListAsync(GetPagedListInput input)
    {
        if (!input.Path.IsNullOrEmpty())
        {
            var list = await _db.Queryable<InterfaceEntity>().Where(x => x.Path.Contains(input.Path)).ToListAsync();
            var pagedList = await _db.Queryable<InterfaceGroupEntity>().Where(x => list.Select(o => o.GroupId).Contains(x.Id)).ToPurestPagedListAsync(input.PageIndex, input.PageSize);
            var result = pagedList.Adapt<PagedList<InterfaceGroupOutput>>();
            foreach (var item in result.Items)
            {
                item.Interfaces = list.Where(l => l.GroupId == item.Id).ToList().Adapt<List<InterfaceOutput>>();
            }
            return result;
        }
        var groups = await _db.Queryable<InterfaceGroupEntity>()
            .WhereIF(!input.GroupName.IsNullOrEmpty(), x => x.Name.Contains(input.GroupName))
            .Includes(x => x.Interfaces)
            .ToPurestPagedListAsync(input.PageIndex, input.PageSize);
        return groups.Adapt<PagedList<InterfaceGroupOutput>>();
    }

    /// <summary>
    /// 导入接口文件
    /// </summary>
    /// <param name="file"></param>
    /// <returns></returns>
    [HttpPost, UnitOfWork]
    public async Task<int> ImportAsync(IFormFile file)
    {
        using StreamReader sr = new(file.OpenReadStream());
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
