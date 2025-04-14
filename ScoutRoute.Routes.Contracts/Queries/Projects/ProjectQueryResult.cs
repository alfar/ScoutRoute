using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoutRoute.Routes.Contracts.Queries.Projects
{
    public class ProjectQueryResult
    {
        public required IEnumerable<ListProjectDto> Projects { get; init; }
    }
}
