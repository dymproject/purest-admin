namespace PurestAdmin.SqlSugar.Entity
{
    /// <summary>
    /// Oauth2用户
    /// </summary>
    [SugarTable("PUREST_OAUTH2_USER")]
    public class OAuth2UserEntity
    {
        /// <summary>
        /// PersistenceId
        /// </summary>
        [SugarColumn(ColumnName = "PERSISTENCE_ID", IsPrimaryKey = true)]
        public long PersistenceId { get; set; }
        /// <summary>
        /// 创建时间
        ///</summary>
        [SugarColumn(ColumnName = "CREATE_TIME")]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// Id
        ///</summary>
        [SugarColumn(ColumnName = "ID")]
        public long Id { get; set; }
        /// <summary>
        /// Name
        /// </summary>
        [SugarColumn(ColumnName = "NAME")]
        public string Name { get; set; }
        /// <summary>
        /// Type
        ///</summary>
        [SugarColumn(ColumnName = "TYPE")]
        public string Type { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        [SugarColumn(ColumnName = "USER_ID")]
        public long? UserId { get; set; }
    }
}
