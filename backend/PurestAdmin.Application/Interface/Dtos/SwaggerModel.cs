// PurestAdmin
// Author:dym
// MIT License
// https://gitee.com/dymproject/purest

namespace PurestAdmin.Application.Interface.Dtos;
public class SwaggerModel
{
    public string Openapi { get; set; }

    public Info Info { get; set; }

    public Dictionary<string, Dictionary<string, PathItem>> Paths { get; set; }

    public Tag[] Tags { get; set; }
}
