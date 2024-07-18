// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

using Microsoft.AspNetCore.SignalR;

using PurestAdmin.Application.UserServices.Dtos;
using PurestAdmin.Core.DataEncryption.Encryptions;
using PurestAdmin.Multiplex.AdminUser;
using PurestAdmin.Multiplex.Contracts.IAdminUser;

namespace PurestAdmin.Application.UserServices;
/// <summary>
/// 用户服务
/// </summary>
[ApiExplorerSettings(GroupName = ApiExplorerGroupConst.SYSTEM)]
public class UserService(ISqlSugarClient db, Repository<UserEntity> userRepository,
    IHubContext<OnlineUserHub, IOnlineUserClient> hubContext, ICacheOnlineUser cacheOnlineUser) : ApplicationService
{
    private readonly ISqlSugarClient _db = db;
    private readonly Repository<UserEntity> _userRepository = userRepository;
    private readonly IHubContext<OnlineUserHub, IOnlineUserClient> _hubContext = hubContext;
    private readonly ICacheOnlineUser _cacheOnlineUser = cacheOnlineUser;
    /// <summary>
    /// 分页查询
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task<PagedList<UserOutput>> GetPagedListAsync(GetPagedListInput input)
    {
        var pagedList = await _db.Queryable<UserEntity>()
            .LeftJoin<UserRoleEntity>((u, ur) => u.Id == ur.UserId)
            .LeftJoin<RoleEntity>((u, ur, r) => ur.RoleId == r.Id)
            .WhereIF(!input.Name.IsNullOrEmpty(), (u) => u.Name.Contains(input.Name))
            .WhereIF(!input.Account.IsNullOrEmpty(), (u) => u.Account.Contains(input.Account, StringComparison.CurrentCultureIgnoreCase))
            .WhereIF(!input.Telephone.IsNullOrEmpty(), (u) => u.Telephone.Contains(input.Telephone))
            .WhereIF(!input.Email.IsNullOrEmpty(), (u) => u.Email.Contains(input.Email))
            .WhereIF(input.Status.HasValue, (u) => u.Status == input.Status)
            .Select((u, ur, r) => new UserEntity
            {
                Id = u.Id.SelectAll(),
                RoleName = r.Name,
                RoleId = r.Id,
                OrganizationName = u.OrganizationId.GetConfigValue<OrganizationEntity>()
            })
            .OrderBy(u => u.Status)
            .OrderByDescending(u => u.CreateTime)
            .ToPurestPagedListAsync(input.PageIndex, input.PageSize);
        return pagedList.Adapt<PagedList<UserOutput>>();
    }

    /// <summary>
    /// 单条查询
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<UserOutput> GetAsync(long id)
    {
        var entity = await _db.Queryable<UserEntity>()
            .LeftJoin<UserRoleEntity>((u, ur) => u.Id == ur.UserId)
            .LeftJoin<RoleEntity>((u, ur, r) => ur.RoleId == r.Id)
            .Where((u) => u.Id == id)
            .Select((u, ur, r) => new UserEntity
            {
                Id = u.Id.SelectAll(),
                RoleName = r.Name,
                RoleId = r.Id,
                OrganizationName = u.OrganizationId.GetConfigValue<OrganizationEntity>()
            }).FirstAsync();
        return entity.Adapt<UserOutput>();
    }

    /// <summary>
    /// 添加
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task<long> AddAsync(AddUserInput input)
    {
        if (input.Password.IsNullOrEmpty())
        {
            input.Password = "123456";
        }
        var entity = input.Adapt<UserEntity>();

        var id = await _userRepository.InsertReturnSnowflakeIdAsync(entity);
        _ = await _db.Insertable(new UserRoleEntity
        {
            UserId = id,
            RoleId = input.RoleId
        }).ExecuteReturnSnowflakeIdAsync();
        return id;
    }

    /// <summary>
    /// 编辑
    /// </summary>
    /// <param name="id"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task PutAsync(long id, PutUserInput input)
    {
        var entity = await _userRepository.GetByIdAsync(id) ?? throw PersistdValidateException.Message(ErrorTipsEnum.NoResult);
        entity = input.Adapt(entity);
        _ = await _db.Deleteable<UserRoleEntity>().Where(x => x.UserId == id).ExecuteCommandAsync();
        _ = await _db.Insertable(new UserRoleEntity
        {
            UserId = id,
            RoleId = input.RoleId
        }).ExecuteReturnSnowflakeIdAsync();
        _ = await _userRepository.UpdateAsync(entity);
    }

    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [UnitOfWork]
    public async Task DeleteAsync(long id)
    {
        var entity = await _userRepository.GetByIdAsync(id) ?? throw PersistdValidateException.Message(ErrorTipsEnum.NoResult);
        if (entity.Account == "admin")
        {
            throw PersistdValidateException.Message("初始化账户禁止删除！");
        }
        await _db.Deleteable<UserRoleEntity>().Where(x => x.UserId == id).ExecuteCommandAsync();
        await _userRepository.DeleteAsync(entity);
    }

    /// <summary>
    /// 账户停用
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task StopAsync(long id)
    {
        var entity = await _userRepository.GetByIdAsync(id) ?? throw PersistdValidateException.Message(ErrorTipsEnum.NoResult);
        if (entity.Account == "admin")
        {
            throw PersistdValidateException.Message("初始化账户不能停用！");
        }
        entity.Status = (int)UserStatusEnum.Stop;
        await _userRepository.UpdateAsync(entity);
        var onlineUsers = _cacheOnlineUser.GetOnlineUsers();
        var connectionIds = onlineUsers.Where(x => x.UserId == id.ToString()).Select(x => x.ConnectionId).ToList();
        await _hubContext.Clients.Clients(connectionIds).Logout();
    }

    /// <summary>
    /// 账户恢复正常
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task NormalAsync(long id)
    {
        var entity = await _userRepository.GetByIdAsync(id) ?? throw PersistdValidateException.Message(ErrorTipsEnum.NoResult);
        entity.Status = (int)UserStatusEnum.Normal;
        await _userRepository.UpdateAsync(entity);
    }

    /// <summary>
    /// 重置密码
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<string> ResetPasswordAsync(long id)
    {
        var entity = await _userRepository.GetByIdAsync(id) ?? throw PersistdValidateException.Message(ErrorTipsEnum.NoResult);
        var randomNumber = new Random().Next(100000, 999999).ToString();
        entity.Password = MD5Encryption.Encrypt(randomNumber);
        _ = await _userRepository.UpdateAsync(entity);
        return randomNumber;
    }
}
