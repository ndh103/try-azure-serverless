using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Api.Core.Entities;
public class Webhook
{
    public string EventName { get; set; }

    public string TriggerUrl { get; set; }

}
