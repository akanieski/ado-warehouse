using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

builder.Environment.ContentRootPath = Directory.GetCurrentDirectory();
builder.Configuration.AddUserSecrets(Assembly.GetExecutingAssembly());

using IHost host = builder.Build();

// Application code should start here.

await host.RunAsync();