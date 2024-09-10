﻿// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

namespace PurestAdmin.Application.AuthServices.Dtos;
/// <summary>
/// 登录模型
/// </summary>
public class LoginInput
{
    /// <summary>
    /// 账号
    /// </summary>
    [Required(ErrorMessage = "账号不能为空"), MinLength(3, ErrorMessage = "账号不能少于 3 位字符")]
    public string Account { get; set; }

    /// <summary>
    /// 密码
    /// </summary>
    [Required(ErrorMessage = "密码不能为空"), MinLength(6, ErrorMessage = "密码不能少于 6 位字符")]
    public string Password { get; set; }
}