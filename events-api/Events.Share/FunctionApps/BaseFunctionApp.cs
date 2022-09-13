using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker.Http;
using Newtonsoft.Json;

namespace Events.Share.FunctionApps;
public class BaseFunctionApp
{
    public HttpResponseData BadRequest<T>(HttpRequestData request, T responseBody)
    {
        var response = request.CreateResponse(HttpStatusCode.BadRequest);
        response.Headers.Add("Content-Type", "application/json; charset=utf-8");

        response.WriteString(JsonConvert.SerializeObject(responseBody));

        return response;
    }

    public HttpResponseData BadRequest(HttpRequestData request, string message)
    {
        var response = request.CreateResponse(HttpStatusCode.BadRequest);
        response.Headers.Add("Content-Type", "application/json; charset=utf-8");

        response.WriteString(message);

        return response;
    }

    public HttpResponseData Unauthorized(HttpRequestData request)
    {
        var response = request.CreateResponse(HttpStatusCode.Unauthorized);
        response.Headers.Add("Content-Type", "application/json; charset=utf-8");

        return response;
    }
}
