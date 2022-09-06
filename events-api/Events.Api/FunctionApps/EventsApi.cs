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

namespace Events.Api.FunctionApps
{
    public class EventsApi
    {
        private readonly ILogger _logger;

        public EventsApi(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<EventsApi>();
        }

        [OpenApiOperation(operationId: "GetEvents", tags: new[] { "Events" }, Summary = "Get Events", Description = "Get Events")]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "x-functions-key", In = OpenApiSecurityLocationType.Header)]
        // [OpenApiSecurity("Authorization", SecuritySchemeType.ApiKey, Name = "Authorization", In = OpenApiSecurityLocationType.Header)]
        // [OpenApiParameter(name: "id", In = ParameterLocation.Path, Required = true, Type = typeof(string), Description = "Id of the event")]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json; charset=utf-8", bodyType: typeof(List<Event>), Description = "The OK response")]
        [Function(nameof(GetEvents))]
        public HttpResponseData GetEvents([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequestData req)
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
    }
}
