// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

namespace PurestAdmin.Application.AuthServices.Dtos;
public class GetCallbackInput
{
    /// <summary>
    /// code
    /// </summary>
    public string Code { get; set; }
    /// <summary>
    /// state
    /// </summary>
    public string State { get; set; }
}
