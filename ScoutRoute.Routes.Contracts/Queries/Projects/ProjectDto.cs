using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoutRoute.Routes.Contracts.Queries.Projects
{
    public class ProjectDto
    {
        public required Guid Id { get; init; }
        public required string Name { get; init; }

        public required IEnumerable<Guid> OwnerIds { get; init; }
    }
}
