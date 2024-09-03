// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurestAdmin.Application.AuthServices.Dtos;
public class AuthorizationHubOutput
{
    public string Id { get; set; }

    public string Name { get; set; }

    public string AccessToken { get; set; }

    public string[] Permissions { get; set; }
}
