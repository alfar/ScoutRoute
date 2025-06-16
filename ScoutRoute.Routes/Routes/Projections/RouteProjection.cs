using Marten.Events.Projections;
using ScoutRoute.Routes.Domain;
using ScoutRoute.Routes.Routes.Domain.Events;

namespace ScoutRoute.Routes.Routes.Projections
{

    public enum RouteStatus
    {
        Active,
        Completed,
        Overfilled
    }

    public sealed record Route(RouteId Id, ProjectId ProjectId, string Name, TeamId? AssignedTeamId, int ExtraStops, string[] Comments, RouteStatus Status);

    public class RouteProjection : EventProjection
    {
        public RouteProjection()
        {
            Project<RouteCreatedEvent>((e, ops) =>
            {
                ops.Store(new Route(e.RouteId, e.ProjectId, e.Name, null, 0, [], RouteStatus.Active));
            });

            ProjectAsync<RouteAssignedToTeamEvent>(async (e, ops) =>
            {
                var route = await ops.LoadAsync<Route>(e.RouteId);

                if (route is not null)
                {
                    ops.Store(route with { AssignedTeamId = e.TeamId });
                }
            });

            ProjectAsync<RouteUnassignedEvent>(async (e, ops) =>
            {
                var route = await ops.LoadAsync<Route>(e.RouteId);

                if (route is not null)
                {
                    ops.Store(route with { AssignedTeamId = null });
                }
            });

        }
    }
}
