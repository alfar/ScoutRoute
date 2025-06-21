using Microsoft.AspNetCore.Routing;

namespace ScoutRoute.Routes.Teams.Endpoints
{
    public static class EndpointExtensions
    {
        public static IEndpointRouteBuilder MapTeamEndpoints(this IEndpointRouteBuilder endpoints)
        {
            endpoints
                .MapAddTeamMember()
                .MapCreateTeam()
                .MapGetAllTeams()
                .MapGetTeam()
                .MapRemoveTeamMember()
                .MapUpdateTeamLead()
                .MapUpdateTeamName()
                .MapUpdateTeamPhone()
                .MapUpdateTeamTrailerType();

            return endpoints;
        }
    }
}
