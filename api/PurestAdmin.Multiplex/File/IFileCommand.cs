// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

namespace PurestAdmin.Multiplex.File;
public interface IFileCommand<T> where T : class
{
    Task<long> SaveAsync(IFormFile file);

    Task<byte[]> ReadAsync(long fileId);

    Task<bool> RemoveAsync(long fileId);
}