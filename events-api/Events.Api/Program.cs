using Events.Api.Common.Configs;
using Microsoft.Azure.Functions.Worker.Extensions.OpenApi.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices((context, services) =>
    {
        var configuration = context.Configuration;
        services.Configure<CosmosDbConfig>(configuration.GetSection(nameof(CosmosDbConfig)));

    })
    .ConfigureOpenApi()
    .Build();

host.Run();
