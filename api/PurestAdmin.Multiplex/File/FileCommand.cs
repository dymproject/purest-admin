// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

using Volo.Abp.BlobStoring;

using Yitter.IdGenerator;

namespace PurestAdmin.Multiplex.File;
public class FileCommand<T>(ISqlSugarClient db, IBlobContainer<T> blobContainer) : IFileCommand<T> where T : class
{
    private readonly ISqlSugarClient _db = db;
    private readonly IBlobContainer<T> _blobContainer = blobContainer;
    public async Task<long> SaveAsync(IFormFile file)
    {
        var id = YitIdHelper.NextId();
        try
        {
            var ext = Path.GetExtension(file.FileName);
            var size = (int)file.Length / 1024 + 1;
            var fileRecord = new FileRecordEntity()
            {
                Id = id,
                FileExt = ext,
                FileName = file.FileName.Trim(),
                FileSize = size
            };
            await _db.Insertable(fileRecord).ExecuteCommandAsync();
            await _blobContainer.SaveAsync(id.ToString(), file.GetAllBytes(), true);
        }
        catch (Exception)
        {
            await _db.Deleteable<FileRecordEntity>().Where(x => x.Id == id).ExecuteCommandAsync();
            throw;
        }
        return id;
    }
    public async Task<byte[]> ReadAsync(long fileId)
    {
        var fileRecord = await _db.Queryable<FileRecordEntity>().Where(x => x.Id == fileId).FirstAsync();
        return fileRecord == null ? [] : await _blobContainer.GetAllBytesAsync(fileId.ToString());
    }
    public async Task<bool> RemoveAsync(long fileId)
    {
        return await _blobContainer.DeleteAsync(fileId.ToString());
    }
}
