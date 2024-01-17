// MIT 许可证
// 版权 © 2023-present https://github.com/dymproject/purest-admin作者 以及贡献者
// 作者或版权持有人都不对任何索赔、损害或其他责任负责，无论这些追责来自合同、侵权或其它行为中，
// 还是产生于、源于或有关于本软件以及本软件的使用或其它处置。

using System.ComponentModel;

using Microsoft.OpenApi.Extensions;

using Volo.Abp;

namespace PurestAdmin.Core.OopsExtension;
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
