using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using WebNursePlanning;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables().AddJsonFile("appsettings.Development.json");
var startup = new Startup(builder.Configuration);

startup.ConfigureServices(builder.Services);

var app = builder.Build();

startup.Configure(app, app.Environment);

app.Run();