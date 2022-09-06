using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Api.Core.Entities;
public class Event
{
    public string Id { get; set; }

    public string Tittle { get; set; }

    public string Description { get; set; }

    public DateTime EventDate { get; set; }

}
