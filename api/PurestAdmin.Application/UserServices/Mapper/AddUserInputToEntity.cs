// MIT 许可证
// 版权 © 2023-present https://github.com/dymproject/purest-admin作者 以及贡献者
// 作者或版权持有人都不对任何索赔、损害或其他责任负责，无论这些追责来自合同、侵权或其它行为中，
// 还是产生于、源于或有关于本软件以及本软件的使用或其它处置。

using PurestAdmin.Application.UserServices.Dtos;
using PurestAdmin.Core.DataEncryption.Encryptions;

namespace PurestAdmin.Application.UserServices.Mapper;
/// <summary>
/// 新增用户映射
/// </summary>
public class AddUserInputToEntity : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        _ = config.ForType<AddUserInput, UserEntity>()
            .IgnoreNullValues(true)
            .Map(dest => dest.Account, src => src.Account.ToLower())
            .Map(dest => dest.Password, src => MD5Encryption.Encrypt(src.Password, false, false));
    }
}
