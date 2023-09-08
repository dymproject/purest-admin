// MIT 许可证
// 版权 © 2023-present dym 和所有贡献者
// 作者或版权持有人都不对任何索赔、损害或其他责任负责，无论这些追责来自合同、侵权或其它行为中，
// 还是产生于、源于或有关于本软件以及本软件的使用或其它处置。

using PurestAdmin.Application.User.Dtos;
using PurestAdmin.Application.User.Services;

namespace PurestAdmin.Application.UserService;
/// <summary>
/// 用户服务
/// </summary>
public class UserService : IUserService, ITransient
{
    private readonly ISqlSugarClient _db;
    private readonly Repository<UserEntity> _userRepository;
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="db"></param>
    /// <param name="userRepository"></param>
    public UserService(ISqlSugarClient db, Repository<UserEntity> userRepository)
    {
        _db = db;
        _userRepository = userRepository;
    }

    /// <summary>
    /// 分页查询
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task<PagedList<UserProfile>> GetPagedListAsync(GetPagedListInput input)
    {
        var pagedList = await _db.Queryable<UserEntity>()
            .LeftJoin<UserRoleEntity>((u, ur) => u.Id == ur.UserId)
            .LeftJoin<RoleEntity>((u, ur, r) => ur.RoleId == r.Id)
            .WhereIF(!input.Name.IsNullOrEmpty(), (u) => u.Name.Contains(input.Name))
            .WhereIF(!input.Account.IsNullOrEmpty(), (u) => u.Account.Contains(input.Account.ToLower()))
            .WhereIF(!input.Telephone.IsNullOrEmpty(), (u) => u.Telephone.Contains(input.Telephone))
            .WhereIF(!input.Email.IsNullOrEmpty(), (u) => u.Email.Contains(input.Email))
            .Select((u, ur, r) => new UserEntity
            {
                Id = u.Id.SelectAll(),
                Role = r
            })
            .OrderByDescending(u => u.CreateTime)
            .ToPurestPagedListAsync(input.PageIndex, input.PageSize);
        return pagedList.Adapt<PagedList<UserProfile>>();
    }

    /// <summary>
    /// 单条查询
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<UserProfile> GetAsync(long id)
    {
        var entity = await _db.Queryable<UserEntity>()
            .LeftJoin<UserRoleEntity>((u, ur) => u.Id == ur.UserId)
            .LeftJoin<RoleEntity>((u, ur, r) => ur.RoleId == r.Id)
            .Where((u) => u.Id == id)
            .Select((u, ur, r) => new UserEntity
            {
                Id = u.Id.SelectAll(),
                Role = r
            }).FirstAsync();
        return entity.Adapt<UserProfile>();
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
    public async Task PutAsync(long id, EditUserInput input)
    {
        var entity = await _userRepository.GetByIdAsync(id) ?? throw Oops.Bah(ErrorTipsEnum.NoResult);
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
    public async Task DeleteAsync(long id)
    {
        var entity = await _userRepository.GetByIdAsync(id) ?? throw Oops.Bah(ErrorTipsEnum.NoResult);
        if (entity.Account == "admin")
        {
            throw Oops.Bah("初始化账户禁止删除！");
        }
        await _db.Deleteable<UserRoleEntity>().Where(x => x.UserId == id).ExecuteCommandAsync();
        await _userRepository.DeleteAsync(entity);
    }

    /// <summary>
    /// 重置密码
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<string> ResetPassword(long id)
    {
        var entity = await _userRepository.GetByIdAsync(id) ?? throw Oops.Bah(ErrorTipsEnum.NoResult);
        var randomNumber = new Random().Next(100000, 999999).ToString();
        entity.Password = AESEncryption.Encrypt(randomNumber, AesKeyConst.Key);
        _ = await _userRepository.UpdateAsync(entity);
        return randomNumber;
    }
}
