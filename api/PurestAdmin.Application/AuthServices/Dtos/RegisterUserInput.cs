using PurestAdmin.Core.DataEncryption.Encryptions;

namespace PurestAdmin.Application.AuthServices.Dtos
{
    /// <summary>
    /// 用户注册模型
    /// </summary>
    public class RegisterUserInput : LoginInput
    {
        /// <summary>
        /// OAuth2User表持久化Id
        /// </summary>
        [Required(ErrorMessage = "OAuth2UserId不能为空")]
        public long OAuth2UserId { get; set; }
        /// <summary>
        /// ConnectionId
        /// </summary>
        [Required(ErrorMessage = "ConnectionId不能为空")]
        public string ConnectionId { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        [Required(ErrorMessage = "用户名不能为空")]
        public string Name { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string Telephone { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }
    }
    /// <summary>
    /// 注册用户映射
    /// </summary>
    public class RegisterUserInputToEntity : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            _ = config.ForType<RegisterUserInput, UserEntity>()
                .IgnoreNullValues(true)
                .Map(dest => dest.Account, src => src.Account.ToLower())
                .Map(dest => dest.Password, src => MD5Encryption.Encrypt(src.Password, false, false));
        }
    }
}
