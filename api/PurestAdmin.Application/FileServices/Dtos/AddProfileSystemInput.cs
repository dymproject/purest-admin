// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

using Microsoft.AspNetCore.Http;

using PurestAdmin.Core.File;

namespace PurestAdmin.Application.FileServices.Dtos;

/// <summary>
/// 系统文件添加
/// </summary>
public class AddProfileSystemInput
{
    /// <summary>
    /// 名称
    /// </summary>
    [Required(ErrorMessage = "名称不能为空"), MaxLength(100, ErrorMessage = "名称最大长度为：20")]
    public string Name { get; set; }
    /// <summary>
    /// 编码
    /// </summary>
    [Required(ErrorMessage = "编码不能为空"), MaxLength(100, ErrorMessage = "编码最大长度为：40")]
    public string Code { get; set; }
    /// <summary>
    /// 文件
    /// </summary>
    [Required(ErrorMessage = "文件不能为空"), FileValidation]
    public IFormFile File { get; set; }
    /// <summary>
    /// 备注
    /// </summary>
    [MaxLength(1000, ErrorMessage = "备注最大长度为：1000")]
    public string Remark { get; set; }
}