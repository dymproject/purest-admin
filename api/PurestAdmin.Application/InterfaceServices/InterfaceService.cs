// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

using System.Reflection;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Controllers;

using Namotion.Reflection;

using PurestAdmin.Application.InterfaceServices.Dtos;

namespace PurestAdmin.Application.InterfaceServices;
/// <summary>
/// 接口服务
/// </summary>
[ApiExplorerSettings(GroupName = ApiExplorerGroupConst.SYSTEM)]
public class InterfaceService(ISqlSugarClient db, IApiDescriptionGroupCollectionProvider apiDescriptionGroupCollectionProvider) : ApplicationService
{
    /// <summary>
    /// db
    /// </summary>
    private readonly ISqlSugarClient _db = db;
    /// <summary>
    /// apiDescriptionGroupCollectionProvider
    /// </summary>
    private readonly IApiDescriptionGroupCollectionProvider _apiDescriptionGroupCollectionProvider = apiDescriptionGroupCollectionProvider;

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
    /// 同步接口
    /// </summary>
    /// <returns></returns>
    [UnitOfWork]
    public async Task AsyncApi()
    {
        var apiDescriptionGroupsItems = _apiDescriptionGroupCollectionProvider.ApiDescriptionGroups.Items.Where(x => !x.GroupName.StartsWith("Abp", StringComparison.OrdinalIgnoreCase));
        var apiDescriptions = apiDescriptionGroupsItems.SelectMany(x => x.Items);
        foreach (var apiDescriptionGroupsItem in apiDescriptionGroupsItems)
        {
            if (!apiDescriptionGroupsItem.Items.Any()) continue;
            var groupEntity = await _db.Queryable<InterfaceGroupEntity>().FirstAsync(x => x.Code == apiDescriptionGroupsItem.GroupName);
            long groupId;
            List<InterfaceEntity> savedInterfaces = [];
            List<InterfaceEntity> newInterfaces = [];
            if (groupEntity == null)
            {
                var summary = ((ControllerActionDescriptor)apiDescriptionGroupsItem.Items[0].ActionDescriptor).ControllerTypeInfo.GetXmlDocsSummary();
                ArgumentNullException.ThrowIfNull(apiDescriptionGroupsItem.GroupName);
                groupId = await _db.Insertable(new InterfaceGroupEntity { Name = summary, Code = apiDescriptionGroupsItem.GroupName }).ExecuteReturnSnowflakeIdAsync();
            }
            else
            {
                groupId = groupEntity.Id;
                savedInterfaces = await _db.Queryable<InterfaceEntity>().Where(x => x.GroupId == groupId).ToListAsync();
            }
            foreach (var apiDescription in apiDescriptionGroupsItem.Items)
            {
                var actionDescriptor = apiDescription.ActionDescriptor as ControllerActionDescriptor;
                var allowAnonymous = actionDescriptor.MethodInfo.GetCustomAttribute<AllowAnonymousAttribute>();
                if (allowAnonymous != null)
                {
                    continue;
                }
                if (!savedInterfaces.Any(x => x.Path == apiDescription.RelativePath && x.RequestMethod == apiDescription.HttpMethod))
                {
                    ArgumentNullException.ThrowIfNull(apiDescription.RelativePath);
                    ArgumentNullException.ThrowIfNull(apiDescription.HttpMethod);
                    var summary = actionDescriptor.MethodInfo.GetXmlDocsSummary();
                    newInterfaces.Add(new InterfaceEntity { GroupId = groupId, Name = summary, Path = apiDescription.RelativePath, RequestMethod = apiDescription.HttpMethod });
                }
            }
            await _db.Insertable(newInterfaces).ExecuteReturnSnowflakeIdListAsync();
            //移除已删除的接口
            var removeInterfaces = savedInterfaces.Where(x =>
            {
                return !apiDescriptionGroupsItem.Items.Any(o => o.RelativePath == x.Path && o.HttpMethod == x.RequestMethod);
            }).ToList();
            await _db.Deleteable<FunctionInterfaceEntity>().Where(x => removeInterfaces.Select(o => o.Id).Contains(x.InterfaceId)).ExecuteCommandAsync();
            await _db.Deleteable(removeInterfaces).ExecuteCommandAsync();
        }
    }
}
