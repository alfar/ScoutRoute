using Marten.Events.Projections;
using ScoutRoute.Routes.Domain;
using ScoutRoute.Routes.Routes.Domain.Events;

namespace ScoutRoute.Routes.Routes.Projections
{
    public enum RouteStatus
    {
        Active,
        Completed,
        Overfilled,
    }

    public sealed record Route(
        RouteId Id,
        ProjectId ProjectId,
        string Name,
        TeamId? AssignedTeamId,
        int ExtraStops,
        string[] Comments,
        RouteStatus Status
    );

    public class RouteProjection : EventProjection
    {
        public RouteProjection()
        {
            Project<RouteCreatedEvent>(
                (e, ops) =>
                {
                    ops.Store(
                        new Route(e.RouteId, e.ProjectId, e.Name, null, 0, [], RouteStatus.Active)
                    );
                }
            );

            ProjectAsync<RouteAssignedToTeamEvent>(
                async (e, ops) =>
                {
                    var route = await ops.LoadAsync<Route>(e.RouteId);

                    if (route is not null)
                    {
                        ops.Store(route with { AssignedTeamId = e.TeamId });
                    }
                }
            );

            ProjectAsync<RouteUnassignedEvent>(
                async (e, ops) =>
                {
                    var route = await ops.LoadAsync<Route>(e.RouteId);

                    if (route is not null)
                    {
                        ops.Store(route with { AssignedTeamId = null });
                    }
                }
            );

            ProjectAsync<RouteUpdatedEvent>(
                async (e, ops) =>
                {
                    var route = await ops.LoadAsync<Route>(e.RouteId);

                    if (route is not null)
                    {
                        ops.Store(route with { Name = e.Name });
                    }
                }
            );

            ProjectAsync<RouteDeletedEvent>(
                async (e, ops) =>
                {
                    var route = await ops.LoadAsync<Route>(e.RouteId);

                    if (route is not null)
                    {
                        ops.Delete<Route>(e.RouteId);
                    }
                }
            );

            ProjectAsync<RouteCompletedEvent>(
                async (e, ops) =>
                {
                    var route = await ops.LoadAsync<Route>(e.RouteId);

                    if (route is not null)
                    {
                        ops.Store(route with { Status = RouteStatus.Completed });
                    }
                }
            );

            ProjectAsync<RouteOverfilledEvent>(
                async (e, ops) =>
                {
                    var route = await ops.LoadAsync<Route>(e.RouteId);

                    if (route is not null)
                    {
                        ops.Store(route with { Status = RouteStatus.Overfilled });
                    }
                }
            );

            ProjectAsync<RouteExtraStopsChangedEvent>(
                async (e, ops) =>
                {
                    var route = await ops.LoadAsync<Route>(e.RouteId);

                    if (route is not null)
                    {
                        ops.Store(route with { ExtraStops = e.ExtraStops });
                    }
                }
            );

            ProjectAsync<RouteCommentAddedEvent>(
                async (e, ops) =>
                {
                    var route = await ops.LoadAsync<Route>(e.RouteId);

                    if (route is not null)
                    {
                        var updatedComments = route.Comments.Append(e.Comment).ToArray();
                        ops.Store(route with { Comments = updatedComments });
                    }
                }
            );
        }
    }
}
