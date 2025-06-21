using Marten.Events.Projections;
using ScoutRoute.Routes.Contracts.ValueTypes;
using ScoutRoute.Routes.Domain;
using ScoutRoute.Routes.Routes.Domain.Events;
using ScoutRoute.Routes.Stops.Domain.Events;

namespace ScoutRoute.Routes.Stops.Projections
{
    public enum StopStatus
    {
        Active = 0,
        PickedUp = 1,
        NotFound = 2,
    }

    public sealed record Stop(
        ProjectId ProjectId,
        StopId Id,
        ContactPerson ContactPerson,
        string Title,
        int Quantity,
        decimal Latitude,
        decimal Longitude,
        string Comment,
        RouteId? RouteId,
        StopStatus Status
    );

    public class StopProjection : EventProjection
    {
        public StopProjection()
        {
            ProjectAsync<RouteCreatedEvent>(
                async (e, ops) =>
                {
                    var newStops = new List<Stop>();
                    foreach (var stopId in e.Stops)
                    {
                        var stop = await ops.LoadAsync<Stop>(stopId);

                        if (stop is not null)
                        {
                            stop = stop with { RouteId = e.RouteId };
                            newStops.Add(stop);
                        }
                    }

                    ops.StoreObjects(newStops);
                }
            );

            Project<StopCreatedEvent>(
                (e, ops) =>
                {
                    ops.Store(
                        new Stop(
                            e.ProjectId,
                            e.StopId,
                            e.ContactPerson,
                            e.Title,
                            e.Quantity,
                            e.Latitude,
                            e.Longitude,
                            e.Comment,
                            null,
                            StopStatus.Active
                        )
                    );
                }
            );

            Project<StopDeletedEvent>(
                (e, ops) =>
                {
                    ops.Delete<Stop>(e.StopId);
                }
            );

            ProjectAsync<RouteStopAddedEvent>(
                async (e, ops) =>
                {
                    var stop = await ops.LoadAsync<Stop>(e.StopId);
                    if (stop is not null)
                    {
                        ops.Store(stop with { RouteId = e.RouteId });
                    }
                }
            );

            ProjectAsync<RouteStopRemovedEvent>(
                async (e, ops) =>
                {
                    var stop = await ops.LoadAsync<Stop>(e.StopId);
                    if (stop is not null)
                    {
                        ops.Store(stop with { RouteId = null });
                    }
                }
            );

            ProjectAsync<StopCompletedEvent>(
                async (e, ops) =>
                {
                    var stop = await ops.LoadAsync<Stop>(e.StopId);
                    if (stop is not null)
                    {
                        ops.Store(stop with { Status = StopStatus.PickedUp });
                    }
                }
            );

            ProjectAsync<StopNotFoundEvent>(
                async (e, ops) =>
                {
                    var stop = await ops.LoadAsync<Stop>(e.StopId);
                    if (stop is not null)
                    {
                        ops.Store(stop with { Status = StopStatus.NotFound });
                    }
                }
            );
        }
    }
}
