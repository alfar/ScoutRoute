using ScoutRoute.Routes.Domain;

namespace ScoutRoute.Routes.Routes.Domain.Events;

public record RouteStopRemovedEvent(ProjectId ProjectId, RouteId RouteId, StopId StopId);
