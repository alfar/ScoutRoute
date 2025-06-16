using ScoutRoute.Routes.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoutRoute.Routes.Stops.Domain.Events
{
    public sealed record StopDeletedEvent(ProjectId ProjectId, StopId StopId);
}
