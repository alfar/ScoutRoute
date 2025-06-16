using Marten;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using ScoutRoute.Routes.Contracts.Commands.Projects;
using ScoutRoute.Routes.Domain;
using ScoutRoute.Routes.Extensions;
using ScoutRoute.Routes.Projects.Domain;
using ScoutRoute.Shared.ValueTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ScoutRoute.Routes.Projects.Endpoints
{
    internal static class ShareProjectEndpoint
    {
        public const string Name = "ShareProject";

        public static IEndpointRouteBuilder MapShareProject(this IEndpointRouteBuilder app)
        {
            app
                .MapPut(Contracts.Endpoints.Endpoints.Projects.ShareProject, static async (Guid projectId, ShareProjectCommand command, IQuerySession query, IDocumentStore store, ClaimsPrincipal user, CancellationToken cancellationToken) =>
                {
                    return await user.LoggedIn(async ownerId =>
                    {
                        var session = await store.LightweightSerializableSessionAsync(cancellationToken);

                        var id = new ProjectId(projectId);

                        var stream = await session.Events.FetchForWriting<ProjectAggregate>(id.GetStreamName(), cancellationToken);

                        var project = stream.Aggregate;

                        if (project is null) return Results.NotFound();

                        var ev = project.AddOwner(new UserId(command.NewOwnerId), ownerId);

                        session.Events.Append(id.GetStreamName(), ev);

                        await session.SaveChangesAsync(cancellationToken);
                        return Results.NoContent();
                    });
                })
                .RequireAuthorization();

            return app;
        }
    }
}
