using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Api.Common.Configs;
public class CosmosDbConfig
{
    public string EndpointUri { get; set; }

    public string PrimaryKey { get; set; }

    public string DatabaseId { get; set; }

    public string ContainerId { get; set; }

}
