using ScoutRoute.Routes.Domain;

namespace ScoutRoute.Routes.Routes.Domain.Events;

public sealed record RouteCompletedEvent(ProjectId ProjectId, RouteId RouteId);
