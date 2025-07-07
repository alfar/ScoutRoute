using Marten;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using ScoutRoute.Routes.Contracts.Queries.Routes;
using ScoutRoute.Routes.Domain;
using ScoutRoute.Routes.Projects.Projections;
using ScoutRoute.Routes.Routes.Mappings;
using ScoutRoute.Routes.Stops.Projections;
using ScoutRoute.Shared.ValueTypes;

namespace ScoutRoute.Routes.Routes.Endpoints
{
    internal static class GetRouteEndpoint
    {
        public const string Name = "GetRoute";

        public static IEndpointRouteBuilder MapGetRoute(this IEndpointRouteBuilder app)
        {
            app.MapGet(
                    Contracts.Endpoints.Endpoints.Routes.Get,
                    async (
                        Guid projectId,
                        Guid routeId,
                        IQuerySession session,
                        CancellationToken cancellationToken
                    ) =>
                    {
                        var route = await session.LoadAsync<Projections.Route>(
                            new RouteId(routeId),
                            cancellationToken
                        );

                        if (route is null)
                            return Results.NotFound();

                        var stops = await session
                            .Query<Stop>()
                            .Where(s => s.RouteId == route.Id)
                            .ToListAsync();

                        return TypedResults.Ok(route.ToDto(stops));
                    }
                )
                .ProducesProblem(StatusCodes.Status404NotFound)
                .Produces<RouteDto>()
                .WithName(Name)
                .WithTags("Routes");

            return app;
        }
    }
}
