// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

namespace PurestAdmin.Application.InterfaceServices.Dtos;
public class SwaggerModel
{
    public string Openapi { get; set; }

    public Info Info { get; set; }

    public Dictionary<string, Dictionary<string, PathItem>> Paths { get; set; }

    public Tag[] Tags { get; set; }
}
public class Info
{
    public string Title { get; set; }

    public string Summary { get; set; }

    public string Description { get; set; }

    public string TermsOfService { get; set; }

    public Contact Contact { get; set; }

    public License License { get; set; }

    public string Version { get; set; }
}

public class License
{
    public string Name { get; set; }

    public string Identifier { get; set; }

    public string Url { get; set; }
}

public class Contact
{
    public string Name { get; set; }
    public string Url { get; set; }
    public string Email { get; set; }
}

public class Tag
{
    public string Name { get; set; }
    public string Description { get; set; }
}
public class PathItem
{
    public string[] Tags { get; set; }

    public string Summary { get; set; }

    public string Description { get; set; }


}