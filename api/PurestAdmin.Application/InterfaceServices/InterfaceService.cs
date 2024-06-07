// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Newtonsoft.Json;

using PurestAdmin.Application.InterfaceServices.Dtos;

namespace PurestAdmin.Application.InterfaceServices;
/// <summary>
/// 接口服务
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
    /// 导入swagger生成的接口文件
    /// </summary>
    /// <param name="file"></param>
    /// <returns></returns>
    [HttpPost, UnitOfWork]
    public async Task ImportAsync(IFormFile file)
    {
        using var sr = new StreamReader(file.OpenReadStream());
        string json = sr.ReadToEnd();
        var swaggerModel = JsonConvert.DeserializeObject<SwaggerModel>(json);
        var interfaceEntities = await _db.Queryable<InterfaceEntity>().ToListAsync();
        foreach (var tag in swaggerModel.Tags)
        {
            var groupEntity = await _db.Queryable<InterfaceGroupEntity>().FirstAsync(x => x.Code == tag.Name);
            var groupId = groupEntity == null ? await _db.Insertable(new InterfaceGroupEntity { Name = tag.Description, Code = tag.Name }).ExecuteReturnSnowflakeIdAsync() : groupEntity.Id;
            var interfaceDetails = new List<InterfaceEntity>();
            foreach (var path in swaggerModel.Paths)
            {
                foreach (var pathItem in path.Value)
                {
                    if (pathItem.Value.Tags.Contains(tag.Name) && !interfaceEntities.Any(x => x.RequestMethod == pathItem.Key && x.Path == path.Key))
                    {
                        interfaceDetails.Add(new InterfaceEntity { GroupId = groupId, Path = path.Key, RequestMethod = pathItem.Key, Name = pathItem.Value.Summary });
                    }
                }
            }
            if (interfaceDetails.Count > 0)
            {
                await _db.Insertable(interfaceDetails).ExecuteReturnSnowflakeIdListAsync();
            }
        }
    }
}
