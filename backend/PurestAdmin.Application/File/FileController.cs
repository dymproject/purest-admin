// MIT 许可证
// 版权 © 2023-present dym 和所有贡献者
// 作者或版权持有人都不对任何索赔、损害或其他责任负责，无论这些追责来自合同、侵权或其它行为中，
// 还是产生于、源于或有关于本软件以及本软件的使用或其它处置。

using Furion.VirtualFileServer;

using PurestAdmin.Application.File.Services;

namespace PurestAdmin.Application.FileCenter;

/// <summary>
/// 文件接口
/// </summary>
[ApiDescriptionSettings(ApiGroupConst.SYSTEM, Description = "系统管理")]
public class FileController : IDynamicApiController
{

    private readonly IFileService _fileService;
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="fileService"></param>
    public FileController(IFileService fileService)
    {
        _fileService = fileService;
    }

    /// <summary>
    /// 下载文件
    /// </summary>
    /// <param name="fileId"></param>
    /// <returns>FileStreamResult</returns>
    [HttpGet, NonUnify, ApiDescriptionSettings(Name = "download")]
    public async Task<IActionResult> FileDownloadAsync([Required(ErrorMessage = "请输入文件id"), Range(1, long.MaxValue), ApiSeat(ApiSeats.ActionStart)] long fileId)
    {
        var (fileName, bytes) = await _fileService.GetFileBytesAsync(fileId);
        return new FileContentResult(bytes, "application/octet-stream")
        {
            FileDownloadName = fileName
        };
    }

    /// <summary>
    /// 上传文件
    /// </summary>
    /// <param name="files"></param>
    /// <returns></returns>
    [HttpPost, NonUnify, ApiDescriptionSettings(Name = "batch-upload")]
    public async Task<List<long>> BatchSaveFileAsync(List<IFormFile> files)
    {
        return await _fileService.BatchSaveFileAsync(files);
    }

    /// <summary>
    /// 上传文件
    /// </summary>
    /// <param name="file"></param>
    /// <returns></returns>
    [HttpPost, NonUnify, ApiDescriptionSettings(Name = "upload")]
    public async Task<long> SaveFileAsync(IFormFile file)
    {
        return await _fileService.SaveFileAsync(file);
    }

    /// <summary>
    /// 将 文件输出为 Url 地址
    /// </summary>
    /// <param name="fileId"></param>
    /// <returns></returns>
    [NonUnify, HttpGet, AllowAnonymous]
    public async Task<IActionResult> GetUrlAsync(long fileId)
    {
        var (fileName, bytes) = await _fileService.GetFileBytesAsync(fileId);
        FS.TryGetContentType(fileName, out string contentType);
        return new FileContentResult(bytes, contentType);
    }
}