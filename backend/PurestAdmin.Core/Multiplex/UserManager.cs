using Furion.DependencyInjection;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;

using PurestAdmin.Core.Const;
using PurestAdmin.Core.Entity;
using PurestAdmin.Core.SqlSugar;

namespace PurestAdmin.Core.Multiplex
{
    /// <summary>
    /// 用户管理接口
    /// </summary>
    public class UserManager : IUserManager, IScoped
    {
        /// <summary>
        /// db
        /// </summary>
        private readonly ISqlSugarClient _db;

        /// <summary>
        /// 用户仓储
        /// </summary>
        private readonly Repository<UserEntity> _userRepository;

        /// <summary>
        /// HttpContext 访问器
        /// </summary>
        private readonly IHttpContextAccessor _httpContextAccessor;

        /// <summary>
        /// 缓存
        /// </summary>
        private readonly IMemoryCache _memoryCache;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="httpContextAccessor"></param>
        /// <param name="userRepository"></param>
        /// <param name="db"></param>
        /// <param name="memoryCache"></param>
        public UserManager(IHttpContextAccessor httpContextAccessor, ISqlSugarClient db, Repository<UserEntity> userRepository, IMemoryCache memoryCache)
        {
            _db = db;
            _httpContextAccessor = httpContextAccessor;
            _userRepository = userRepository;
            _memoryCache = memoryCache;
        }

        /// <summary>
        /// 获取用户 Id
        /// </summary>
        public long UserId { get => long.Parse(_httpContextAccessor.HttpContext?.User.FindFirst(ClaimConst.USERID)?.Value ?? throw Oops.Bah("令牌超时，请重新登录！")); }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        public UserEntity User { get => _userRepository.GetById(UserId) ?? throw Oops.Bah("用户可能被删除了，请退出系统重试！"); }

        /// <summary>
        /// 查询用户功能
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<List<FunctionEntity>> GetFunctionsAsync(long userId)
        {
            return await _db.Queryable<RoleFunctionEntity>()
              .LeftJoin<FunctionEntity>((rf, f) => rf.FunctionId == f.Id)
              .Where((rf, f) => rf.RoleId == SqlFunc.Subqueryable<UserRoleEntity>().Where(u => u.UserId == userId).Select(u => u.RoleId))
              .Select((rf, f) => f)
              .ToListAsync();
        }
        /// <summary>
        /// 查询用户权限
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<List<InterfaceEntity>> GetInterfacesAsync(long userId)
        {
            return await _db.Queryable<UserRoleEntity>()
                .RightJoin<RoleFunctionEntity>((ur, rf) => ur.RoleId == rf.RoleId)
                .RightJoin<FunctionInterfaceEntity>((ur, rf, fi) => rf.FunctionId == fi.FunctionId)
                .RightJoin<InterfaceEntity>((ur, rf, fi, inter) => fi.InterfaceId == inter.Id)
                .Select((ur, rf, fi, inter) => inter)
                .ToListAsync();
        }
    }
}