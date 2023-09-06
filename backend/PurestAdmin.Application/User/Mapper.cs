// PurestAdmin
// Author:dym
// MIT License
// https://gitee.com/dymproject/purest

using PurestAdmin.Application.User.Dtos;
using PurestAdmin.Core.Consts;

namespace PurestAdmin.Application.User;
/// <summary>
/// 新增用户映射
/// </summary>
public class Mapper : IRegister
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="config"></param>
    public void Register(TypeAdapterConfig config)
    {
        config.ForType<AddUserInput, UserEntity>()
            .IgnoreNullValues(true)
            .Map(dest => dest.Account, src => src.Account.ToLower())
            .Map(dest => dest.Password, src => AESEncryption.Encrypt(src.Password, AesKeyConst.Key));
    }
}
