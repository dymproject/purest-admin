// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

using System.Reflection;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.Options;

using Volo.Abp.AspNetCore.Mvc.Conventions;
using Volo.Abp.Reflection;

namespace PurestAdmin.Api.Host.Options;

public class AdminConventionalRouteBuilder(IOptions<AbpConventionalControllerOptions> options) : ConventionalRouteBuilder(options)
{
    public override string Build(string rootPath, string controllerName, ActionModel action, string httpMethod, ConventionalControllerSetting? configuration)
    {
        string apiRoutePrefix = GetApiRoutePrefix(action, configuration);
        string controllerName2 = NormalizeUrlControllerName(rootPath, controllerName, action, httpMethod, configuration);
        string text = $"{apiRoutePrefix}/{rootPath}/{NormalizeControllerNameCase(controllerName2, configuration)}";
        var idParameterModel = action.Parameters.FirstOrDefault(p => p.ParameterName == "id");
        if (idParameterModel != null)
        {
            if (TypeHelper.IsPrimitiveExtended(idParameterModel.ParameterType, includeNullables: true, includeEnums: true))
            {
                if (!idParameterModel.ParameterType.CustomAttributes.Any(x => x.GetType() == typeof(FromQueryAttribute)
                || x.GetType() == typeof(FromBodyAttribute) || x.GetType() == typeof(FromHeaderAttribute)
                || x.GetType() == typeof(FromFormAttribute)))
                    text += "/{id}";
            }
            else
            {
                PropertyInfo[] properties = idParameterModel.ParameterType.GetProperties(BindingFlags.Instance | BindingFlags.Public);
                foreach (PropertyInfo property in properties)
                {
                    text = text + "/{" + NormalizeIdPropertyNameCase(property, configuration) + "}";
                }
            }
        }
        string text2 = NormalizeUrlActionName(rootPath, controllerName, action, httpMethod, configuration);
        if (!text2.IsNullOrEmpty())
        {
            List<ParameterModel> list = action.Parameters.Where((p) => p.ParameterName.EndsWith("Id", StringComparison.Ordinal)).ToList();
            if (list.Count == 1)
            {
                text = text + "/{" + NormalizeSecondaryIdNameCase(list[0], configuration) + "}";
            }
            text = text + "/" + NormalizeActionNameCase(text2, configuration);
        }

        return text;
    }
}
