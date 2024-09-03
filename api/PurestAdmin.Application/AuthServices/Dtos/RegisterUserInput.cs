namespace PurestAdmin.Application.AuthServices.Dtos
{
    /// <summary>
    /// 用户注册模型
    /// </summary>
    public class RegisterUserInput : LoginInput
    {
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
}
