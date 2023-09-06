// PurestAdmin
// Author:dym
// MIT License
// https://gitee.com/dymproject/purest

namespace PurestAdmin.Application.Interface.Dtos;
public class GetPagedListInput: PaginationParams
{
    public string GroupName { get; set; }
    
    /// <summary>
    /// 地址
    /// </summary>
    public string Path { get; set; }
}
