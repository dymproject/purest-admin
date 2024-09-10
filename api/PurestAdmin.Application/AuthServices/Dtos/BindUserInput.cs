// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

namespace PurestAdmin.Application.AuthServices.Dtos;
public class BindUserInput : LoginInput
{
    /// <summary>
    /// ConnectionId
    /// </summary>
    [Required(ErrorMessage = "ConnectionId不能为空")]
    public string ConnectionId { get; set; }
    /// <summary>
    /// OAuth2User表持久化Id
    /// </summary>
    [Required(ErrorMessage = "OAuth2UserId不能为空")]
    public long OAuth2UserId { get; set; }
}
