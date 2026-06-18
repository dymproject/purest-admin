// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;

using PurestAdmin.Core.Cache;
using PurestAdmin.Multiplex.Contracts.IAdminUser;

namespace PurestAdmin.Application.ChatServices;
/// <summary>
/// 用户授权服务
/// </summary>
[ApiExplorerSettings(GroupName = ApiExplorerGroupConst.SYSTEM)]
public class ChatService(IAdminCache cache, Kernel kernel, ICurrentUser currentUser) : ApplicationService
{
    /// <summary>
    /// cache
    /// </summary>
    private readonly IAdminCache _cache = cache;
    /// <summary>
    /// kernel
    /// </summary>
    private readonly Kernel _kernel = kernel;
    /// <summary>
    /// 当前用户
    /// </summary>
    private readonly ICurrentUser _currentUser = currentUser;

    /// <summary>
    /// AI对话
    /// </summary>
    /// <param name="userQuestion"></param>
    /// <returns></returns>
    public async Task<string> PostAsync(string userQuestion)
    {
        string cacheKey = $"chat_history_{_currentUser.Id}";

        var history = _cache.Get<ChatHistory>(cacheKey) ?? [];

        var result = await _kernel.ChatWithPurestFunctionsAsync(userQuestion, history: history);

        _cache.Set(cacheKey, history, TimeSpan.FromMinutes(30));

        return result;
    }
}
