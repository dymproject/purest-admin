// PurestAdmin
// Author:dym
// MIT License
// https://gitee.com/dymproject/purest

namespace PurestAdmin.Application.File.Dtos
{
    /// <summary>
    /// 文件上传配置
    /// </summary>
    public class FileUploadConfig
    {
        /// <summary>
        /// 限制上传大小（M）
        /// </summary>
        public int LimitFileSize { get; set; } = 5;
        /// <summary>
        /// 文件存放路径
        /// </summary>
        public string SavedDirectory { get; set; } = "files";
    }
}
