// MIT 许可证
// 版权 © 2023-present https://github.com/dymproject/purest-admin作者 以及贡献者
// 作者或版权持有人都不对任何索赔、损害或其他责任负责，无论这些追责来自合同、侵权或其它行为中，
// 还是产生于、源于或有关于本软件以及本软件的使用或其它处置。

using System.Text;
using System.Text.RegularExpressions;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

using PurestAdmin.Api.Host;
using PurestAdmin.Api.Host.FriendlyException;
using PurestAdmin.Api.Host.Handler;
using PurestAdmin.Application;
using PurestAdmin.Core;

using Volo.Abp;
using Volo.Abp.AspNetCore.ExceptionHandling;
using Volo.Abp.AspNetCore.Mvc.AntiForgery;
using Volo.Abp.AspNetCore.Mvc.ApiExploring;
using Volo.Abp.AspNetCore.Mvc.Conventions;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;
using Volo.Abp.Swashbuckle;
using Volo.Abp.VirtualFileSystem;

namespace PurestAdmin.WebApi.Host
{
    [DependsOn(typeof(AbpSwashbuckleModule),
        typeof(AbpAutofacModule),
        typeof(AdminCoreModule),
        typeof(AdminAppModule))]
    public class AdminHostModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();
            var hostingEnvironment = context.Services.GetHostingEnvironment();

            CoinfigureControllers(context, hostingEnvironment);
            ConfigureAuthorizationServices(context, configuration);
            ConfigureSwaggerServices(context);
            ConfigureVirtualFileSystem(context);
            ConfigureCors(context, configuration);
        }

        private void CoinfigureControllers(ServiceConfigurationContext context, IWebHostEnvironment hostEnvironment)
        {
            //HTTP状态代码映射（配合oops返回400）
            context.Services.AddSingleton<IHttpExceptionStatusCodeFinder, PurestHttpExceptionStatusCodeFinder>();

            //发送异常详情到客户端true(发送)/false（不发送）
            context.Services.Configure<AbpExceptionHandlingOptions>(options =>
            {
                options.SendExceptionsDetailsToClients = hostEnvironment.IsDevelopment();
            });
            //路由生成规则
            context.Services.AddTransient<IConventionalRouteBuilder, AdminConventionalRouteBuilder>();
            //解决string类型默认增加require的标记，详见官方文档
            context.Services.AddControllers(options =>
            {
                options.Filters.AddService<PurestAbpExceptionsFilter>();
                options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true;
            });
        }
        private void ConfigureAuthorizationServices(ServiceConfigurationContext context, IConfiguration configuration)
        {
            context.Services.AddSingleton<IAuthorizationHandler, AuthorizationHandler>();
            context.Services.AddSingleton<IAuthorizationMiddlewareResultHandler, AuthorizationMiddlewareResultHandler>();
            context.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    var key = Encoding.UTF8.GetBytes(configuration["JWTSettings:IssuerSigningKey"] ?? "49BA59ABBE56E05749BA59ABBE56E057");
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ClockSkew = TimeSpan.FromSeconds(10),
                        IssuerSigningKey = new SymmetricSecurityKey(key)
                    };
                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            var accessToken = context.Request.Query["access_token"];
                            // If the request is for our hub...
                            var path = context.HttpContext.Request.Path;
                            if (!string.IsNullOrEmpty(accessToken) &&
                                (path.StartsWithSegments("/hubs")))
                            {
                                // Read the token out of the query string
                                context.Token = accessToken;
                            }
                            return Task.CompletedTask;
                        }
                    };
                });
            //解决post 400错误
            Configure<AbpAntiForgeryOptions>(options =>
            {
                options.AutoValidate = false;
            });
        }
        private void ConfigureSwaggerServices(ServiceConfigurationContext context)
        {
            Configure<AbpRemoteServiceApiDescriptionProviderOptions>((options) =>
            {
                options.SupportedResponseTypes.Clear();
            });
            context.Services.AddAbpSwaggerGen(
                options =>
                {
                    options.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
                    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "PurestAdmin.Application.xml"));
                    options.DocInclusionPredicate((docName, description) => true);
                    options.CustomSchemaIds(type => type.FullName);
                    options.HideAbpEndpoints();
                    options.SchemaFilter<HideAbpSchemaFilter>();
                    options.MapType<DateTime>(() => new OpenApiSchema { Type = "string" });
                    //swagger授权方案定义
                    options.AddSecurityDefinition("Bearer",  //定义授权方案的名称
                        new OpenApiSecurityScheme()
                        {
                            Description = "JWT Authorization header using the Bearer scheme.",
                            BearerFormat = "JWT",
                            Scheme = "bearer",
                            Name = "Authorization",  //参数名--与标题头的参名相同
                            In = ParameterLocation.Header,  //参数放在Header中
                            Type = SecuritySchemeType.Http,  //类型是apikey

                        });
                    //加载授权方案
                    options.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference=new OpenApiReference
                                {
                                    Id="Bearer",
                                    Type= ReferenceType.SecurityScheme
                                }
                            },
                            Array.Empty<string>()
                        }
                    });
                });
        }
        private void ConfigureVirtualFileSystem(ServiceConfigurationContext context)
        {
            var hostingEnvironment = context.Services.GetHostingEnvironment();

            if (hostingEnvironment.IsDevelopment())
            {
                Configure<AbpVirtualFileSystemOptions>(options =>
                {
                    options.FileSets.ReplaceEmbeddedByPhysical<AdminCoreModule>(
                        Path.Combine(hostingEnvironment.ContentRootPath,
                            $"..{Path.DirectorySeparatorChar}PurestAdmin.Core"));
                    //options.FileSets.ReplaceEmbeddedByPhysical<AdminDomainModule>(
                    //    Path.Combine(hostingEnvironment.ContentRootPath,
                    //        $"..{Path.DirectorySeparatorChar}PurestAdmin.Domain"));
                    //options.FileSets.ReplaceEmbeddedByPhysical<AdminAppModule>(
                    //    Path.Combine(hostingEnvironment.ContentRootPath,
                    //        $"..{Path.DirectorySeparatorChar}PurestAdmin.Application.Contracts"));
                });
            }
        }
        private void ConfigureCors(ServiceConfigurationContext context, IConfiguration configuration)
        {
            context.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder
                        .WithOrigins(configuration["App:CorsOrigins"]?
                            .Split(",", StringSplitOptions.RemoveEmptyEntries)
                            .Select(o => o.RemovePostFix("/"))
                            .ToArray() ?? [])
                        .WithAbpExposedHeaders()
                        .SetIsOriginAllowedToAllowWildcardSubdomains()
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
            });
            //解决The cookie 'XSRF-TOKEN' has set 'SameSite=None' and must also set 'Secure'警告
            context.Services.Configure<CookiePolicyOptions>(options =>
            {
                options.MinimumSameSitePolicy = SameSiteMode.Unspecified;
                options.OnAppendCookie = cookieContext =>
                {
                    if (cookieContext.CookieOptions.SameSite == SameSiteMode.None)
                    {
                        if (cookieContext.Context.Request.Scheme != "https")
                        {
                            cookieContext.CookieOptions.SameSite = SameSiteMode.Unspecified;
                        }
                    }
                };
                options.OnDeleteCookie = cookieContext =>
                {
                    if (cookieContext.CookieOptions.SameSite == SameSiteMode.None)
                    {
                        if (cookieContext.Context.Request.Scheme != "https")
                        {
                            cookieContext.CookieOptions.SameSite = SameSiteMode.Unspecified;
                        }
                    }
                };
            });

        }
        public override void OnApplicationInitialization(ApplicationInitializationContext context)
        {
            var app = context.GetApplicationBuilder();
            var env = context.GetEnvironment();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseHsts();
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "API");
                    var responseInterceptor = @" (res) => {
                            const token = res.headers.accesstoken;
                            console.log(token);
                            if(token){localStorage.setItem('token', token);}
                            return res;
                        }";
                    var requestInterceptor = @" (req) => {
                            req.headers.Authorization = 'Bearer ' + localStorage.getItem('token');
                            console.log(localStorage.getItem('token'));
                            return req;
                        }";
                    options.UseResponseInterceptor(Regex.Replace(responseInterceptor, @"\s+", " "));
                    options.UseRequestInterceptor(Regex.Replace(requestInterceptor, @"\s+", " "));
                });
            }

            app.UseCookiePolicy();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseConfiguredEndpoints(options =>
            {
                options.MapControllers().RequireAuthorization();
            });
        }
    }
}
