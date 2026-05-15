// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

namespace PurestAdmin.Multiplex.AI;

/// <summary>
/// 包含AI服务的配置选项。
/// </summary>
public class AIOptions
{
    /// <summary>
    /// 大规模模型的API端点。
    /// </summary>
    public string Endpoint { get; set; }

    /// <summary>
    /// 大规模模型的API密钥。
    /// </summary>
    public string ApiKey { get; set; }

    /// <summary>
    /// 使用的大规模模型名称。
    /// </summary>
    public string ModelName { get; set; }
}
