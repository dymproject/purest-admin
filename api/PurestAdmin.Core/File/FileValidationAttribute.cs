// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace PurestAdmin.Core.File;
public class FileValidationAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null)
        {
            return new ValidationResult($"不能为空");
        }
        var file = value as IFormFile;
        var configuration = validationContext.GetRequiredService<IConfiguration>();
        var fileSystemOptions = configuration.GetSection("FileSystemOptions").Get<FileSystemOptions>();
        if (file.Length == 0)
        {
            return new ValidationResult($"检测到文件内容为空");
        }
        //限制文件上传大小默认100M
        if (file.Length / 1024 > fileSystemOptions.LimitSize * 1024)
        {
            return new ValidationResult($"文件不能超过{fileSystemOptions.LimitSize}M");
        }
        return ValidationResult.Success;
    }
}
