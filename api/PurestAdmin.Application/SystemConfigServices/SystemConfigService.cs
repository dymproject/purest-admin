// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

using PurestAdmin.Application.SystemConfigServices.Dtos;
using PurestAdmin.Core.Oops;

namespace PurestAdmin.Application.SystemConfigServices;
/// <summary>
/// 系统配置表服务
/// </summary>
[ApiExplorerSettings(GroupName = ApiExplorerGroupConst.SYSTEM)]
public class SystemConfigService(ISqlSugarClient db, Repository<SystemConfigEntity> systemConfigRepository) : ApplicationService
{
    private readonly ISqlSugarClient _db = db;
    private readonly Repository<SystemConfigEntity> _systemConfigRepository = systemConfigRepository;

    /// <summary>
    /// 分页查询
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task<PagedList<SystemConfigOutput>> GetPagedListAsync(GetPagedListInput input)
    {
        var pagedList = await _db.Queryable<SystemConfigEntity>()
            .WhereIF(!input.Name.IsNullOrEmpty(), x => x.Name.Contains(input.Name))
            .WhereIF(!input.ConfigCode.IsNullOrEmpty(), x => x.ConfigCode.Contains(input.ConfigCode))
            .OrderByDescending(x => x.CreateTime)
            .ToPurestPagedListAsync(input.PageIndex, input.PageSize);
        return pagedList.Adapt<PagedList<SystemConfigOutput>>();
    }

    /// <summary>
    /// 单条查询
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<SystemConfigOutput> GetAsync(long id)
    {
        var entity = await _systemConfigRepository.GetByIdAsync(id);
        return entity.Adapt<SystemConfigOutput>();
    }

    /// <summary>
    /// 添加
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task<long> AddAsync(AddSystemConfigInput input)
    {
        var entity = input.Adapt<SystemConfigEntity>();
        return await _systemConfigRepository.InsertReturnSnowflakeIdAsync(entity);
    }

    /// <summary>
    /// 编辑
    /// </summary>
    /// <param name="id"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task PutAsync(long id, AddSystemConfigInput input)
    {
        var entity = await _systemConfigRepository.GetByIdAsync(id) ?? throw Oops.Bah(ErrorTipsEnum.NoResult);
        var newEntity = input.Adapt(entity);
        _ = await _systemConfigRepository.UpdateAsync(newEntity);
    }

    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task DeleteAsync(long id)
    {
        var entity = await _systemConfigRepository.GetByIdAsync(id) ?? throw Oops.Bah(ErrorTipsEnum.NoResult);
        _ = await _systemConfigRepository.DeleteAsync(entity);
    }

}
