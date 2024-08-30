using Mapster;

using PurestAdmin.Multiplex.Contracts.IAdminUser.OAuth2;

namespace PurestAdmin.Multiplex.AdminUser
{
    public class OAuth2UserManager(ISqlSugarClient db) : IOAuth2UserManager, IScopedDependency
    {
        private readonly ISqlSugarClient _db = db;


        public async Task<OAuth2UserEntity> GetOAuth2UserPersistenceIdAsync(OAuth2UserInfo userInfo)
        {
            var entity = await _db.Queryable<OAuth2UserEntity>().FirstAsync(x => x.Id == userInfo.Id && x.Type == userInfo.Type);
            if (entity == null)
            {
                entity = userInfo.Adapt<OAuth2UserEntity>();
                entity.Id = await _db.Insertable(entity).ExecuteReturnSnowflakeIdAsync();
            }
            return entity;
        }
    }
}
