using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoutRoute.Routes.Domain.Events
{
    internal class DomainEvent
    {
        public required ProjectId ProjectId { get; init; }

        public string GetStreamName() => $"Project:{ProjectId}";
    }
}
