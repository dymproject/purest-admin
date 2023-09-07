// MIT 许可证
// 版权 © 2023-present dym 和所有贡献者
// 作者或版权持有人都不对任何索赔、损害或其他责任负责，无论这些追责来自合同、侵权或其它行为中，
// 还是产生于、源于或有关于本软件以及本软件的使用或其它处置。

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
