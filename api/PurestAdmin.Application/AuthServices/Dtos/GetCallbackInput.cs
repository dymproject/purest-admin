// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

namespace PurestAdmin.Application.AuthServices.Dtos;
public class GetCallbackInput
{
    /// <summary>
    /// code
    /// </summary>
    [Required(ErrorMessage = "code不能为空")]
    public string Code { get; set; }
    /// <summary>
    /// state
    /// </summary>
    [Required(ErrorMessage = "State不能为空")]
    public string State { get; set; }
}
