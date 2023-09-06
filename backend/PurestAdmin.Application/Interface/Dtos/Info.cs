// PurestAdmin
// Author:dym
// MIT License
// https://gitee.com/dymproject/purest

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurestAdmin.Application.Interface.Dtos;
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

