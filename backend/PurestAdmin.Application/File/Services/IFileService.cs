// PurestAdmin
// Author:dym
// MIT License
// https://gitee.com/dymproject/purest

namespace PurestAdmin.Application.File.Services
{
    /// <summary>
    /// 文件上传接口
    /// </summary>
    public interface IFileService
    {
        /// <summary>
        /// 文件批量保存
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        Task<List<long>> BatchSaveFileAsync(List<IFormFile> files);
        /// <summary>
        /// 单文件保存
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        Task<long> SaveFileAsync(IFormFile file);
        /// <summary>
        /// 获取文件名称和内容byte数组
        /// </summary>
        /// <param name="fileId"></param>
        /// <returns></returns>
        Task<(string, byte[])> GetFileBytesAsync(long fileId);
    }
}
