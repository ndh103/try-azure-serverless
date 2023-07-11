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

public class EventsApi
{
    private readonly ILogger _logger;

    public EventsApi(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<EventsApi>();
    }

    [OpenApiOperation(operationId: "CheckJwt", tags: new[] { "CheckJwt" }, Summary = "CheckJwt", Description = "CheckJwt")]
    // [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "x-functions-key", In = OpenApiSecurityLocationType.Header)]
    [OpenApiSecurity("Authorization", SecuritySchemeType.ApiKey, Name = "Authorization", In = OpenApiSecurityLocationType.Header)]
    // [OpenApiParameter(name: "id", In = ParameterLocation.Path, Required = true, Type = typeof(string), Description = "Id of the event")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json; charset=utf-8", bodyType: typeof(List<Event>), Description = "The OK response")]
    [Function(nameof(CheckJwt))]
    public HttpResponseData CheckJwt([HttpTrigger(AuthorizationLevel.Function, "get", Route = "jwts")] HttpRequestData req)
    {
        var response = req.CreateResponse(HttpStatusCode.OK);
        response.Headers.Add("Content-Type", "application/json; charset=utf-8");

        var jwt = GetJwtFromHeader(req);

        response.WriteString(JsonConvert.SerializeObject(new { appJwt = jwt }));
        return response;
    }

    [OpenApiOperation(operationId: "GetEvents", tags: new[] { "Events" }, Summary = "Get Events", Description = "Get Events")]
    // [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "x-functions-key", In = OpenApiSecurityLocationType.Header)]
    [OpenApiSecurity("Authorization", SecuritySchemeType.ApiKey, Name = "Authorization", In = OpenApiSecurityLocationType.Header)]
    // [OpenApiParameter(name: "id", In = ParameterLocation.Path, Required = true, Type = typeof(string), Description = "Id of the event")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json; charset=utf-8", bodyType: typeof(List<Event>), Description = "The OK response")]
    [Function(nameof(GetEvents))]
    public HttpResponseData GetEvents([HttpTrigger(AuthorizationLevel.Function, "get", Route = "events")] HttpRequestData req)
    {
        var response = req.CreateResponse(HttpStatusCode.OK);
        response.Headers.Add("Content-Type", "application/json; charset=utf-8");

        var events = new List<Event>()
        {
            new Event()
            {
                Id  = Guid.NewGuid().ToString(),
                Description =" first event",
                EventDate = new DateTime(2022, 9, 6),
                Title= "Clean up the air conditioner"
            },
            new Event()
            {
                Id  = Guid.NewGuid().ToString(),
                Description =" second event",
                EventDate = new DateTime(2022, 9, 7),
                Title= "Fix the water purifier"
            }
        };

        response.WriteString(JsonConvert.SerializeObject(events));

        return response;
    }

    [OpenApiOperation(operationId: "AddEvents", tags: new[] { "Events" }, Summary = "Add Events", Description = "Add Events")]
    // [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "x-functions-key", In = OpenApiSecurityLocationType.Header)]
    [OpenApiSecurity("Authorization", SecuritySchemeType.ApiKey, Name = "Authorization", In = OpenApiSecurityLocationType.Header)]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json; charset=utf-8", bodyType: typeof(List<Event>), Description = "The OK response")]
    [Function(nameof(AddEvent))]
    public async Task<HttpResponseData> AddEvent([HttpTrigger(AuthorizationLevel.Function, "post", Route = "events")] HttpRequestData req)
    {
        var response = req.CreateResponse(HttpStatusCode.OK);
        response.Headers.Add("Content-Type", "application/json; charset=utf-8");

        var test = await req.ReadFromJsonAsync<Event>();

        response.WriteString(JsonConvert.SerializeObject(test));

        return response;
    }

    private string GetJwtFromHeader(HttpRequestData req)
    {
        req.Headers.TryGetValues("Authorization", out var authorizationHeaders);

        if (authorizationHeaders == null || !authorizationHeaders.Any())
        {
            return string.Empty;
        }

        var authHeaderValue = authorizationHeaders.First().ToString();
        if (!authHeaderValue.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
        {
            // Scheme is not Bearer
            return string.Empty;
        }

        var token = authHeaderValue.Substring("Bearer ".Length).Trim();
        return token;
    }
}
