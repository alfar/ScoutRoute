using ScoutRoute.Routes.Domain;

namespace ScoutRoute.Routes.Routes.Domain.Events;

public record RouteStopAddedEvent(ProjectId ProjectId, RouteId RouteId, StopId StopId);
