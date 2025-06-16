using ScoutRoute.Routes.Contracts.Queries.Teams;
using ScoutRoute.Routes.Routes.Mappings;
using ScoutRoute.Routes.Routes.Projections;
using ScoutRoute.Routes.Teams.Projections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoutRoute.Routes.Teams.Mappings
{
    internal static class TeamMappings
    {
        public static IEnumerable<ListTeamDto> ToListDtos(this IEnumerable<Team> teams) => teams.Select((t, i) => t.ToListDto(i));

        public static ListTeamDto ToListDto(this Team team, int index) => new() { Id = team.Id.Value, Name = team.Name ?? $"Team {index}" };

        public static TeamDto ToDto(this Team team, IEnumerable<Route> routes) => new() { Id = team.Id.Value, Name = team.Name, TeamLead = team.TeamLead, Phone = team.Phone, TrailerType = (int?)team.TrailerType, Members = team.Members.ToArray(), Routes = routes.ToListDtos() };
    }
}
