// MIT 许可证
// 版权 © 2023-present dym 和所有贡献者
// 作者或版权持有人都不对任何索赔、损害或其他责任负责，无论这些追责来自合同、侵权或其它行为中，
// 还是产生于、源于或有关于本软件以及本软件的使用或其它处置。

using Furion;

using PurestAdmin.Application.File.Dtos;

using Yitter.IdGenerator;

namespace PurestAdmin.Application.File.Services
{
    /// <summary>
    /// 文件上传服务
    /// </summary>
    public class FileService : IFileService, ITransient
    {
        /// <summary>
        /// 文件记录仓储
        /// </summary>
        private readonly Repository<FileRecordEntity> _fileRecordRepository;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="fileRecordRepository"></param>
        public FileService(Repository<FileRecordEntity> fileRecordRepository)
        {
            _fileRecordRepository = fileRecordRepository;
        }

        /// <summary>
        /// 获取文件名称和内容byte数组
        /// </summary>
        /// <param name="fileId"></param>
        /// <returns></returns>
        public async Task<(string, byte[])> GetFileBytesAsync(long fileId)
        {
            var fileRecord = await _fileRecordRepository.GetByIdAsync(fileId);
            if (!System.IO.File.Exists(fileRecord.FullPath)) throw Oops.Bah("文件不存在~");
            FileStream fs = new(fileRecord.FullPath, FileMode.Open);
            //定义字节流
            byte[] bytes = new byte[fs.Length];
            //文件读取到字节流中
            await fs.ReadAsync(bytes);
            //关闭读取
            fs.Close();
            return (fileRecord.FileName, bytes);
        }

        /// <summary>
        /// 批量上传文件
        /// </summary>
        /// <param name="files"></param>
        /// <returns></returns>
        public async Task<List<long>> BatchSaveFileAsync(List<IFormFile> files)
        {
            var fileUploadConfig = App.GetConfig<FileUploadConfig>("FileUploadConfig");
            //文件大小不能超过5M
            if (files.Any(x => x.Length > fileUploadConfig.LimitFileSize * 1024 * 1024))
            {
                throw Oops.Bah($"上传的文件不能超过{fileUploadConfig.LimitFileSize}M");
            }
            var directory = Path.Combine(AppContext.BaseDirectory, fileUploadConfig.SavedDirectory);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            var fileRecords = new List<FileRecordEntity>();
            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    var id = YitIdHelper.NextId();
                    var ext = Path.GetExtension(formFile.FileName);
                    var uniqueFileName = id + ext;
                    var size = (int)formFile.Length / 1024 + 1;
                    var fullPath = Path.Combine(directory, uniqueFileName, DateTime.Now.ToString("yyyy-MM-dd"));
                    //物理存储
                    using (var stream = System.IO.File.Create(fullPath))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                    //入库
                    fileRecords.Add(new FileRecordEntity()
                    {
                        Id = id,
                        FullPath = fullPath,
                        FileExt = ext,
                        FileName = formFile.FileName,
                        FileSize = size,
                    });
                }
            }
            await _fileRecordRepository.InsertRangeAsync(fileRecords);
            return fileRecords.Select(x => x.Id).ToList();
        }

        /// <summary>
        /// 单个上传文件
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public async Task<long> SaveFileAsync(IFormFile file)
        {
            var fileUploadConfig = App.GetConfig<FileUploadConfig>("FileUploadConfig");
            //文件大小不能超过5M
            if (file.Length > fileUploadConfig.LimitFileSize * 1024 * 1024)
            {
                throw Oops.Bah($"上传的文件不能超过{fileUploadConfig.LimitFileSize}M");
            }
            var directory = Path.Combine(AppContext.BaseDirectory, fileUploadConfig.SavedDirectory);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            if (file.Length > 0)
            {
                var id = YitIdHelper.NextId();
                var ext = Path.GetExtension(file.FileName);
                var uniqueFileName = id + ext;
                var size = (int)file.Length / 1024 + 1;
                var fullPath = Path.Combine(directory, uniqueFileName, DateTime.Now.ToString("yyyy-MM-dd"));
                //物理存储
                using (var stream = System.IO.File.Create(fullPath))
                {
                    await file.CopyToAsync(stream);
                }
                //入库
                return await _fileRecordRepository.InsertReturnSnowflakeIdAsync(new FileRecordEntity()
                {
                    Id = id,
                    FullPath = fullPath,
                    FileExt = ext,
                    FileName = file.FileName,
                    FileSize = size,
                });
            }
            throw Oops.Bah("文件上传异常~");
        }
    }
}
