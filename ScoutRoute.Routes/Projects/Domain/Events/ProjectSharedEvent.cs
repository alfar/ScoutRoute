using ScoutRoute.Routes.Domain;
using ScoutRoute.Shared.ValueTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoutRoute.Routes.Projects.Domain.Events
{
    public sealed record ProjectSharedEvent(ProjectId ProjectId, UserId NewOwnerId, UserId OwnerId);
}
