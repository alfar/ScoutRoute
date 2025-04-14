using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoutRoute.Routes.Domain.Events
{
    internal class ProjectCreatedEvent : DomainEvent
    {
        public required string Name { get; init; }
        public required PersonId OwnerId { get; init; }
    }
}
