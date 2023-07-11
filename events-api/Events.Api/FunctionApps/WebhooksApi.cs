using System.Collections.Generic;
using System.Net;
using Events.Api.Core.Entities;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

namespace Events.Api.FunctionApps;

public class WebhooksApi
{
    private readonly ILogger _logger;

    public WebhooksApi(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<WebhooksApi>();
    }

    [OpenApiOperation(operationId: "Add Webhooks", tags: new[] { "Webhooks" }, Summary = "Add Webhooks", Description = "Add Webhooks")]
    // [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "x-functions-key", In = OpenApiSecurityLocationType.Header)]
    [OpenApiSecurity("Authorization", SecuritySchemeType.ApiKey, Name = "Authorization", In = OpenApiSecurityLocationType.Header)]
    [OpenApiRequestBody("application/json; charset=utf-8", bodyType: typeof(Webhook))]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.Created, contentType: "application/json; charset=utf-8", bodyType: typeof(List<Event>), Description = "The OK response")]
    [Function(nameof(AddWebhook))]
    public async Task<HttpResponseData> AddWebhook([HttpTrigger(AuthorizationLevel.Function, "post", Route = "webhooks")] HttpRequestData req)
    {
        var response = req.CreateResponse(HttpStatusCode.Created);
        response.Headers.Add("Content-Type", "application/json; charset=utf-8");
        var randomGuid = Guid.NewGuid().ToString();
        response.Headers.Add("Location", $"/webhooks/{randomGuid}");

        var webhook = await req.ReadFromJsonAsync<Webhook>();

        response.WriteString(JsonConvert.SerializeObject(webhook));

        return response;
    }

    [OpenApiOperation(operationId: "Remove Webhooks", tags: new[] { "Webhooks" }, Summary = "Remove Webhooks", Description = "Remove Webhooks")]
    // [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "x-functions-key", In = OpenApiSecurityLocationType.Header)]
    [OpenApiSecurity("Authorization", SecuritySchemeType.ApiKey, Name = "Authorization", In = OpenApiSecurityLocationType.Header)]
    [OpenApiParameter(name: "webhookId", In = ParameterLocation.Path, Required = true, Type = typeof(string), Description = "Id of the webhook")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json; charset=utf-8", bodyType: typeof(List<Event>), Description = "The OK response")]
    [Function(nameof(RemoveWebhook))]
    public async Task<HttpResponseData> RemoveWebhook([HttpTrigger(AuthorizationLevel.Function, "delete", Route = "webhooks/{webhookId}")] HttpRequestData req, string webhookId)
    {
        var response = req.CreateResponse(HttpStatusCode.OK);
        response.Headers.Add("Content-Type", "application/json; charset=utf-8");

        response.WriteString(JsonConvert.SerializeObject(new
        {
            webhookId = webhookId
        }));

        return response;
    }

}
