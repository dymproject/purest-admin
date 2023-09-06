// PurestAdmin
// Author:dym
// MIT License
// https://gitee.com/dymproject/purest

using System.Reflection;

using Furion;

namespace PurestAdmin.Web.Entry
{
    public class SingleFilePublish : ISingleFilePublish
    {
        public Assembly[] IncludeAssemblies()
        {
            return Array.Empty<Assembly>();
        }

        public string[] IncludeAssemblyNames()
        {
            return new[]
            {
            "PurestAdmin.Application",
            "PurestAdmin.Core",
            "PurestAdmin.Web.Core"
        };
        }
    }
}