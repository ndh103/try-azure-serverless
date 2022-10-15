using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Events.Authorization.Api.Core.Entities
{
    public class ResponseContent
    {
        public const string ApiVersion = "1.0.0";

        public ResponseContent()
        {
            Version = ApiVersion;
            Action = "Continue";
        }

        public ResponseContent(string action, string userMessage)
        {
            Version = ApiVersion;
            Action = action;
            UserMessage = userMessage;
            if (action == "ValidationError")
            {
                Status = "400";
            }
        }

        [JsonPropertyName("version")]
        public string Version { get; }


        [JsonPropertyName("action")]
        public string Action { get; set; }


        [JsonPropertyName("userMessage")]
        public string? UserMessage { get; set; }


        [JsonPropertyName("status")]
        public string? Status { get; set; }


        [JsonPropertyName("extension_MyCustomClaim")]
        public string MyCustomClaim { get; set; } = string.Empty;
    }
}
