using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Azure.Core.Serialization;
using Events.Authorization.Api.Common.Configs;
using Events.Authorization.Api.Core.Entities;
using Events.Share.FunctionApps;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

namespace Events.Authorization.Api.FunctionApps;

public class RolesApi : BaseFunctionApp
{
    private readonly ILogger _logger;
    private const string BaseRoute = "authorization";
    private readonly ApiConnectorConfig config;
    private readonly AzureB2CConfig _azureB2CConfig;

    public RolesApi(ILoggerFactory loggerFactory, IOptions<ApiConnectorConfig> apiConnectorOptions, IOptions<AzureB2CConfig> azureB2COptions)
    {
        _logger = loggerFactory.CreateLogger<RolesApi>();
        config = apiConnectorOptions.Value;
        _azureB2CConfig = azureB2COptions.Value;
    }

    [OpenApiOperation(operationId: "GetRoles", tags: new[] { "Roles" }, Summary = "Get Roles", Description = "Get Roles")]
    [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "x-functions-key", In = OpenApiSecurityLocationType.Header)]
    // [OpenApiSecurity("Authorization", SecuritySchemeType.ApiKey, Name = "Authorization", In = OpenApiSecurityLocationType.Header)]
    [OpenApiRequestBody("application/json; charset=utf-8", typeof(RequestConnector))]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json; charset=utf-8", bodyType: typeof(ResponseContent), Description = "The OK response")]
    [Function(nameof(GetRoles))]
    public async Task<HttpResponseData> GetRoles([HttpTrigger(AuthorizationLevel.Function, "post", Route = $"{BaseRoute}/roles")] HttpRequestData request)
    {
        var response = request.CreateResponse(HttpStatusCode.OK);
        response.Headers.Add("Content-Type", "application/json; charset=utf-8");

        // Check HTTP basic authorization
        if (!IsAuthorizedUsingUnsecureBasicAuth(request))
        {
            _logger.LogWarning("HTTP basic authentication validation failed.");
            return Unauthorized(request);
        }

        var requestConnector = await request.ReadFromJsonAsync<RequestConnector>();

        _logger.LogInformation($"requestConnector: {JsonSerializer.Serialize(requestConnector)}");

        // If input data is null, show block page
        if (requestConnector == null)
        {
            _logger.LogInformation("Request Connector is null");
            return BadRequest(request, new ResponseContent("ShowBlockPage", "There was a problem with your request."));
        }

        string clientId = _azureB2CConfig.ClientId;

        _logger.LogInformation($"ClientId: {clientId}");

        if (!clientId.Equals(requestConnector.ClientId))
        {
            _logger.LogWarning("HTTP clientId is not authorized.");
            return Unauthorized(request);
        }

        // If email claim not found, show block page. Email is required and sent by default.
        if (requestConnector.Email == null || requestConnector.Email == "" || requestConnector.Email.Contains("@") == false)
        {
            return BadRequest(request, new ResponseContent("ShowBlockPage", "Email name is mandatory."));
        }

        var result = new ResponseContent
        {
            // use the objectId of the email to get the user specfic claims
            MyCustomClaim = $"user admin"
        };

        response.WriteString(System.Text.Json.JsonSerializer.Serialize(result));

        return response;
    }

    private bool IsAuthorizedUsingUnsecureBasicAuth(HttpRequestData request)
    {
        string username = config.BasicAuthUserName;
        string password = config.BasicAuthPassword;

        // Check if the HTTP Authorization header exist
        if (!request.Headers.Contains("Authorization"))
        {
            _logger.LogWarning("Missing HTTP basic authentication header.");
            return false;
        }

        // Read the authorization header
        var auth = request.Headers.GetValues("Authorization").First();

        // Ensure the type of the authorization header id `Basic`
        if (!auth.StartsWith("Basic "))
        {
            _logger.LogWarning("HTTP basic authentication header must start with 'Basic '.");
            return false;
        }

        // Get the the HTTP basinc authorization credentials
        var cred = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(auth.Substring(6))).Split(':');

        // Evaluate the credentials and return the result
        return cred[0] == username && cred[1] == password;
    }
}
