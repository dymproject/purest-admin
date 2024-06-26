// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

using System.Web;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Net.Http.Headers;

using PurestAdmin.Application.ProfileSystemServices.Dtos;
using PurestAdmin.Core.File;
using PurestAdmin.Core.File.Containers;
using PurestAdmin.Core.Oops;

using Volo.Abp;
using Volo.Abp.Validation;


namespace PurestAdmin.Application.ProfileSystemServices;

/// <summary>
/// 系统文件服务
/// </summary>
/// <param name="db"></param>
/// <param name="fileCommand"></param>
/// <param name="httpContextAccessor"></param>
/// <param name="objectValidator"></param>
[ApiExplorerSettings(GroupName = ApiExplorerGroupConst.SYSTEM)]
public class ProfileSystemService(ISqlSugarClient db, IFileCommand<ProfileSystemContainer> fileCommand,
    IHttpContextAccessor httpContextAccessor, IObjectValidator objectValidator) : ApplicationService
{
    private readonly ISqlSugarClient _db = db;
    private readonly IFileCommand<ProfileSystemContainer> _fileCommand = fileCommand;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    private readonly IObjectValidator _objectValidator = objectValidator;
    /// <summary>
    /// 分页查询
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public async Task<PagedList<ProfileSystemOutput>> GetPagedListAsync(GetPagedListInput input)
    {
        var pagedList = await _db.Queryable<ProfileSystemEntity>()
            .LeftJoin<FileRecordEntity>((x, r) => x.FileId == r.Id)
            .WhereIF(!input.Name.IsNullOrEmpty(), x => x.Name.Contains(input.Name))
            .WhereIF(!input.Code.IsNullOrEmpty(), x => x.Code.Contains(input.Code))
            .Select((x, r) => new ProfileSystemEntity
            {
                Id = x.Id.SelectAll(),
                FileName = r.FileName,
                FileSize = r.FileSize,
            })
            .OrderByDescending(x => x.CreateTime)
            .ToPurestPagedListAsync(input.PageIndex, input.PageSize);
        return pagedList.Adapt<PagedList<ProfileSystemOutput>>();
    }

    /// <summary>
    /// 单条查询
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<ProfileSystemOutput> GetAsync(long id)
    {
        var entity = await _db.Queryable<ProfileSystemEntity>().FirstAsync(x => x.Id == id) ?? throw Oops.Bah(ErrorTipsEnum.NoResult);
        return entity.Adapt<ProfileSystemOutput>();
    }

    /// <summary>
    /// 添加（文件流式上传）
    /// </summary>
    /// <returns></returns>
    [DisableFormValueModelBinding, RemoteService(false)]
    public async Task<long> AddByStreamAsync()
    {
        var formAccumulator = new KeyValueAccumulator();
        var contentType = _httpContextAccessor.HttpContext.Request.ContentType ?? throw Oops.Bah("请求被拒绝,请求内容为空");
        if (!contentType.Contains("multipart/", StringComparison.OrdinalIgnoreCase))
        {
            throw Oops.Bah("请求被拒绝，http 415");
        }
        var boundary = HeaderUtilities.RemoveQuotes(MediaTypeHeaderValue.Parse(contentType).Boundary).Value;
        ArgumentNullException.ThrowIfNull(boundary);
        var reader = new MultipartReader(boundary, _httpContextAccessor.HttpContext.Request.Body);
        var section = await reader.ReadNextSectionAsync();
        while (section != null)
        {
            var hasContentDispositionHeader = ContentDispositionHeaderValue.TryParse(section.ContentDisposition, out var contentDisposition);

            if (contentDisposition != null && hasContentDispositionHeader)
            {
                if (contentDisposition.DispositionType.Equals("form-data") && (!string.IsNullOrEmpty(contentDisposition.FileName.Value)
                    || !string.IsNullOrEmpty(contentDisposition.FileNameStar.Value)))
                {
                    //这里上传文件到文件系统，返回文件ID,添加到dictionary
                    formAccumulator.Append("fileId", "");
                }
                else
                {
                    var key = HeaderUtilities.RemoveQuotes(contentDisposition.Name).Value;
                    ArgumentNullException.ThrowIfNull(key);
                    using var streamReader = new StreamReader(section.Body);
                    var value = await streamReader.ReadToEndAsync();
                    formAccumulator.Append(key, value);
                }
            }
            section = await reader.ReadNextSectionAsync();
        }
        var entity = formAccumulator.GetResults().Adapt<ProfileSystemEntity>();
        return await _db.Insertable(entity).ExecuteReturnSnowflakeIdAsync();
    }

    /// <summary>
    /// 添加
    /// </summary>
    /// <returns></returns>
    public async Task<long> AddAsync([FromForm] AddProfileSystemInput input)
    {
        await _objectValidator.ValidateAsync(input);
        var entity = input.Adapt<ProfileSystemEntity>();
        entity.FileId = await _fileCommand.SaveAsync(input.File);
        return await _db.Insertable(entity).ExecuteReturnSnowflakeIdAsync();
    }

    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [UnitOfWork]
    public async Task DeleteAsync(long id)
    {
        var profileSystemEntity = await _db.Queryable<ProfileSystemEntity>().FirstAsync(x => x.Id == id) ?? throw Oops.Bah(ErrorTipsEnum.NoResult);
        var fileRecordEntity = await _db.Queryable<FileRecordEntity>().FirstAsync(x => x.Id == profileSystemEntity.FileId) ?? throw Oops.Bah("未找到对应的文件记录！");
        await _fileCommand.RemoveAsync(profileSystemEntity.FileId);
        await _db.Deleteable(profileSystemEntity).ExecuteCommandAsync();
        await _db.Deleteable(fileRecordEntity).ExecuteCommandAsync();
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
        var bytes = await _fileCommand.ReadAsync(fileId);
        return new FileContentResult(bytes, "application/octet-stream")
        {
            FileDownloadName = HttpUtility.UrlEncode(fileRecord.FileName)
        };
    }
}
