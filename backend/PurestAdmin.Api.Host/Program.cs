// MIT 许可证
// 版权 © 2023-present https://github.com/dymproject/purest-admin作者 以及贡献者
// 作者或版权持有人都不对任何索赔、损害或其他责任负责，无论这些追责来自合同、侵权或其它行为中，
// 还是产生于、源于或有关于本软件以及本软件的使用或其它处置。

using PurestAdmin.WebApi.Host;

using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseAutofac();

builder.Host.UseSerilog((context, services, configuration) =>
{
    var template = "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}";
    configuration.MinimumLevel.Override("Microsoft", LogEventLevel.Warning); // 排除Microsoft的日志
    configuration.MinimumLevel.Debug();
    configuration.WriteTo.Console();
    configuration.WriteTo.File($"{AppContext.BaseDirectory}\\Logs\\.txt", rollingInterval: RollingInterval.Day, outputTemplate: template);
});

builder.Services.ReplaceConfiguration(builder.Configuration);

builder.Services.AddApplication<AdminHostModule>();

var app = builder.Build();
app.InitializeApplication();
app.Run();