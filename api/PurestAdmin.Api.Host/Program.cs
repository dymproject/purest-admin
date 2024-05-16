// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

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
    configuration.WriteTo.File($"{AppContext.BaseDirectory}/logs/.txt", rollingInterval: RollingInterval.Day, outputTemplate: template);
});

builder.Services.ReplaceConfiguration(builder.Configuration);

builder.Services.AddApplication<AdminHostModule>();

var app = builder.Build();
app.InitializeApplication();
app.Run();