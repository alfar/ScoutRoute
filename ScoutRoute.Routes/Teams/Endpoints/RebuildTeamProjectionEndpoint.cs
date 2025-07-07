using Marten;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace ScoutRoute.Routes.Teams.Endpoints
{
    internal static class RebuildTeamProjectionEndpoint
    {
        public const string Name = "RebuildTeam";

        public static IEndpointRouteBuilder MapRebuildTeamProjection(this IEndpointRouteBuilder app)
        {
            app
                .MapGet(Contracts.Endpoints.Endpoints.Teams.Rebuild, async (IDocumentStore store) =>
                {
                    using var daemon = await store.BuildProjectionDaemonAsync();

                    await daemon.RebuildProjectionAsync("ScoutRoute.Routes.Teams.Projections.TeamProjection", CancellationToken.None);
                })
                .WithName("Rebuild")
                .WithTags("Team");

            return app;
        }
    }
}
