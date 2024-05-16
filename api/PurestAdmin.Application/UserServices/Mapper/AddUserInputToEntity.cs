// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

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
