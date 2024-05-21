// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

using System.Web;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

using PurestAdmin.Core.File;

using Volo.Abp.BlobStoring;

using Yitter.IdGenerator;

namespace PurestAdmin.Application.FileServices;

public class ProfileSystemService(IConfiguration configuration, ISqlSugarClient db, IBlobContainer<ProfileSystemContainer> blobContainer) : ApplicationService
{
    private readonly ISqlSugarClient _db = db;
    private readonly IBlobContainer<ProfileSystemContainer> _blobContainer = blobContainer;
    private readonly IConfiguration _configuration = configuration;
    /// <summary>
    /// 单文件上传
    /// </summary>
    /// <param name="file"></param>
    /// <returns></returns>
    public async Task<long> UploadAsync([Required(ErrorMessage = "文件不能为空！")] IFormFile file)
    {
        if (file.Length == 0)
        {
            throw Oops.Bah("检测到空文件！");
        }
        //限制文件上传大小默认100M
        var limitSize = _configuration["FileStorageOptions:LimitSize"] ?? "100";
        if (file.Length / 1024 > int.Parse(limitSize) * 1024)
        {
            throw Oops.Bah($"上传的文件不能超过{limitSize}");
        }
        var id = YitIdHelper.NextId();
        var ext = Path.GetExtension(file.FileName);
        var size = (int)file.Length / 1024 + 1;

        var fileRecord = new FileRecordEntity()
        {
            Id = id,
            FileExt = ext,
            FileName = file.FileName.Trim(),
            FileSize = size
        };
        await _blobContainer.SaveAsync(id.ToString(), file.GetAllBytes(), true);
        await _db.Insertable(fileRecord).ExecuteCommandAsync();
        return id;
    }

    /// <summary>
    /// 文件下载
    /// </summary>
    /// <param name="fileId"></param>
    /// <returns>FileStreamResult</returns>
    [HttpGet]
    public async Task<IActionResult> DownloadAsync([Required(ErrorMessage = "请输入文件id"), Range(1, long.MaxValue)] long fileId)
    {
        var fileRecord = await _db.Queryable<FileRecordEntity>().FirstAsync(x => x.Id == fileId);
        var bytes = await _blobContainer.GetAllBytesAsync(fileId.ToString());
        return new FileContentResult(bytes, "application/octet-stream")
        {
            FileDownloadName = HttpUtility.UrlEncode(fileRecord.FileName)
        };
    }
}
