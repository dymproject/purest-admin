﻿// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

using System.Runtime.InteropServices;

namespace PurestAdmin.Application.AuthServices.Dtos;
/// <summary>
/// 系统信息
/// </summary>
/// <remarks>引用来源https://gitee.com/whuanle/reflection_and_properties/blob/master/%E5%8F%8D%E5%B0%84%E7%89%B9%E6%80%A7%E5%BA%94%E7%94%A8%E5%9C%BA%E6%99%AF1.cs</remarks>
public class GetSystemPlatformInfoOutput
{
    public string FrameworkDescription { get { return RuntimeInformation.FrameworkDescription; } }
    [Display(Name = "操作系统")]
    public string OSDescription { get { return RuntimeInformation.OSDescription; } }
    [Display(Name = "操作系统版本")]
    public string OSVersion { get { return Environment.OSVersion.ToString(); } }
    [Display(Name = "平台架构")]
    public string OSArchitecture { get { return RuntimeInformation.OSArchitecture.ToString(); } }
    [Display(Name = "机器名称")]
    public string MachineName { get { return Environment.MachineName; } }
    [Display(Name = "Web程序核心框架版本")]
    public string Version { get { return Environment.Version.ToString(); } }
}
