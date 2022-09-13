using Events.Authorization.Api.Common.Configs;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices((context, services) =>
    {
        var configuration = context.Configuration;
        services.Configure<ApiConnectorConfig>(configuration.GetSection(nameof(ApiConnectorConfig)));
        services.Configure<AzureB2CConfig>(configuration.GetSection(nameof(AzureB2CConfig)));

    })
    .Build();

host.Run();
