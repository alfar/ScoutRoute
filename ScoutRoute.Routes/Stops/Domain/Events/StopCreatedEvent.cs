using ScoutRoute.Routes.Contracts.ValueTypes;
using ScoutRoute.Routes.Domain;
using ScoutRoute.Shared.ValueTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoutRoute.Routes.Stops.Domain.Events
{
    public sealed record StopCreatedEvent(ProjectId ProjectId, StopId StopId, ContactPerson ContactPerson, string Title, int Quantity, decimal Latitude, decimal Longitude, string Comment, UserId UserId)
    {
    }
}
