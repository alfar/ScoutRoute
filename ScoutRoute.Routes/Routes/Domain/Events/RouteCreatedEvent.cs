using ScoutRoute.Routes.Domain;

namespace ScoutRoute.Routes.Routes.Domain.Events;

public sealed record RouteCreatedEvent(
    ProjectId ProjectId,
    RouteId RouteId,
    string Name,
    StopId[] Stops
);
