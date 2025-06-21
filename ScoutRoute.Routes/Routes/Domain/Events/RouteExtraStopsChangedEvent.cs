using ScoutRoute.Routes.Domain;

namespace ScoutRoute.Routes.Routes.Domain.Events;

public sealed record RouteExtraStopsChangedEvent(
    ProjectId ProjectId,
    RouteId RouteId,
    int ExtraStops
);
