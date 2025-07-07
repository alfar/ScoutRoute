using ScoutRoute.Routes.Domain;

namespace ScoutRoute.Routes.Routes.Domain.Events;

public sealed record RouteOverfilledEvent(ProjectId ProjectId, RouteId RouteId);
