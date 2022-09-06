using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Events.Api.Core.Entities;
[JsonObject(NamingStrategyType = typeof(CamelCaseNamingStrategy))]
public class Event
{
    public string Id { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public DateTime EventDate { get; set; }

}
