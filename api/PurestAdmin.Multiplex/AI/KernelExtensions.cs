// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI; // 注意：如果是千问，通常映射到此命名空间

public static class KernelExtensions
{
    /// <summary>
    /// AI 自动调度业务函数执行
    /// </summary>
    /// <param name="kernel">内核实例</param>
    /// <param name="prompt">用户输入</param>
    /// <param name="systemMessage">系统约束指令（可选）</param>
    /// <param name="history">对话历史（可选，用于多轮对话）</param>
    public static async Task<string> ChatWithPurestFunctionsAsync(
        this Kernel kernel,
        string prompt,
        string? systemMessage = "你是一个业务智能助手，请根据提供的工具回答问题。如果工具无法处理，请如实告知。",
        ChatHistory? history = null)
    {
        // 1. 获取聊天完成服务
        var chatService = kernel.GetRequiredService<IChatCompletionService>();

        // 2. 准备对话历史（如果没传则创建新的）
        history ??= [];
        if (history.Count == 0 && !string.IsNullOrEmpty(systemMessage))
        {
            history.AddSystemMessage(systemMessage);
        }

        history.AddUserMessage(prompt);

        // 3. 配置自动函数调用策略
        // 针对阿里云千问等模型，建议明确指定 OpenAIPromptExecutionSettings
        var settings = new OpenAIPromptExecutionSettings
        {
            // 核心：开启自动函数调用
            FunctionChoiceBehavior = FunctionChoiceBehavior.Auto(),
            // 降低随机性，使业务逻辑更严谨
            Temperature = 0.3f,
            // 确保输出不被意外切断
            MaxTokens = 2000
        };

        // 4. 执行请求
        // 这里会自动循环处理：AI要求调函数 -> 执行C#代码 -> 结果返给AI -> AI生成总结文本
        var response = await chatService.GetChatMessageContentAsync(
            history,
            executionSettings: settings,
            kernel: kernel);

        // 5. 将 AI 的回复存入历史，以便下次对话使用
        history.AddAssistantMessage(response.Content ?? "");

        return response.Content ?? string.Empty;
    }
}