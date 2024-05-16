// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

using System.ComponentModel;

using Microsoft.OpenApi.Extensions;

using Volo.Abp;

namespace PurestAdmin.Core;
public static class Oops
{
    /// <summary>
    /// 抛出业务异常信息
    /// </summary>
    /// <param name="errorMessage">异常消息</param>
    /// <returns>异常实例</returns>
    public static BusinessException Bah(string errorMessage)
    {
        return new UserFriendlyException(message: errorMessage);
    }
    /// <summary>
    /// 抛出业务异常信息
    /// </summary>
    /// <param name="errorMessage">异常消息</param>
    /// <returns>异常实例</returns>
    public static BusinessException Bah(Enum tipsEnum)
    {
        var description = tipsEnum.GetAttributeOfType<DescriptionAttribute>() ?? throw Bah("未找到对应异常的枚举描述");
        return new UserFriendlyException(description.Description);
    }
}
