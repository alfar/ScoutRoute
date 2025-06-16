using ScoutRoute.Routes.Contracts.Queries.Routes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoutRoute.Routes.Contracts.Queries.Teams
{
    public class TeamDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? TeamLead { get; set; }
        public string? Phone { get; set; }
        public int? TrailerType { get; set; }
        public string[] Members { get; set; } = [];

        public IEnumerable<ListRouteDto> Routes { get; set; } = [];
    }
}
