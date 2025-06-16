using ScoutRoute.Routes.Domain;

namespace ScoutRoute.Routes.Routes.Domain.Events;

public sealed record RouteUnassignedEvent(ProjectId ProjectId, RouteId RouteId);
