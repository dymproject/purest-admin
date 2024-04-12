// MIT 许可证
// 版权 © 2023-present https://github.com/dymproject/purest-admin作者 以及贡献者
// 作者或版权持有人都不对任何索赔、损害或其他责任负责，无论这些追责来自合同、侵权或其它行为中，
// 还是产生于、源于或有关于本软件以及本软件的使用或其它处置。

using PurestAdmin.Application.Organization.Dtos;

namespace PurestAdmin.Application.OrganizationServices;
/// <summary>
/// 组织机构服务
/// </summary>
public class OrganizationService(Repository<OrganizationEntity> organizationRepository, ICurrentUser currentUser) : ApplicationService
{
    private readonly Repository<OrganizationEntity> _organizationRepository = organizationRepository;
    private readonly ICurrentUser _currentUser = currentUser;

    /// <summary>
    /// 分页查询
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task<PagedList<OrganizationOutput>> GetPagedListAsync(GetPagedListInput input)
    {
        var organizations = await _currentUser.GetOrganizationTreeAsync();
        var organizationOutputs = organizations.First().Children.Adapt<List<OrganizationOutput>>();
        return organizationOutputs.ToPurestPagedList(input.PageIndex, input.PageSize);
    }

    /// <summary>
    /// 单条查询
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<OrganizationOutput> GetAsync(long id)
    {
        var entity = await _organizationRepository.GetByIdAsync(id);
        return entity.Adapt<OrganizationOutput>();
    }

    /// <summary>
    /// 添加
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task<long> AddAsync(AddOrganizationInput input)
    {
        var entity = input.Adapt<OrganizationEntity>();
        return await _organizationRepository.InsertReturnSnowflakeIdAsync(entity);
    }

    /// <summary>
    /// 编辑
    /// </summary>
    /// <param name="id"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task PutAsync(long id, AddOrganizationInput input)
    {
        var entity = await _organizationRepository.GetByIdAsync(id) ?? throw Oops.Bah(ErrorTipsEnum.NoResult);
        var newEntity = input.Adapt(entity);
        _ = await _organizationRepository.UpdateAsync(newEntity);
    }

    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task DeleteAsync(long id)
    {
        var entity = await _organizationRepository.GetByIdAsync(id) ?? throw Oops.Bah(ErrorTipsEnum.NoResult);
        await _organizationRepository.DeleteAsync(entity);
    }

}
