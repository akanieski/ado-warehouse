using Microsoft.Extensions.Hosting;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureHostConfiguration(hostConf => hostConf.AddUserSecrets(Assembly.GetExecutingAssembly()))
    .ConfigureServices(services => services.AddDbContext<WorkItemProcessorDbContext>())
    .Build();

host.Run();
