// Copyright © 2023-present https://github.com/dymproject/purest-admin作者以及贡献者

using PurestAdmin.Api.Host;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseAutofac();

builder.Services.ReplaceConfiguration(builder.Configuration);

builder.Services.AddApplication<AdminHostModule>();

var app = builder.Build();
app.InitializeApplication();
app.Run();