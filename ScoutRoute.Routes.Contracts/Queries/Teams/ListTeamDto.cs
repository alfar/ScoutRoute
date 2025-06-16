using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoutRoute.Routes.Contracts.Queries.Teams
{
    public class ListTeamDto
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
    }
}
