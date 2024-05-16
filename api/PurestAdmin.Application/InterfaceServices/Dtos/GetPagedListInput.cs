// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

namespace PurestAdmin.Application.InterfaceServices.Dtos;
public class GetPagedListInput : PaginationParams
{
    public string GroupName { get; set; }

    /// <summary>
    /// 地址
    /// </summary>
    public string Path { get; set; }
}
