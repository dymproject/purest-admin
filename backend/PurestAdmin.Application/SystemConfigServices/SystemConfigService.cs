// MIT 许可证
// 版权 © 2023-present https://github.com/dymproject/purest-admin作者 以及贡献者
// 作者或版权持有人都不对任何索赔、损害或其他责任负责，无论这些追责来自合同、侵权或其它行为中，
// 还是产生于、源于或有关于本软件以及本软件的使用或其它处置。

using PurestAdmin.Application.SystemConfig.Dtos;

namespace PurestAdmin.Application.SystemConfigServices;
/// <summary>
/// 系统配置表服务
/// </summary>
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
