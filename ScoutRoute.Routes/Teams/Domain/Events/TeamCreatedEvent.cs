using ScoutRoute.Routes.Domain;
using ScoutRoute.Shared.ValueTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoutRoute.Routes.Teams.Domain.Events
{
    public sealed record TeamCreatedEvent(ProjectId ProjectId, TeamId TeamId);
}
