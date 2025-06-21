using ScoutRoute.Routes.Domain;

namespace ScoutRoute.Routes.Routes.Domain.Events;

public sealed record RouteStatusResetEvent(ProjectId ProjectId, RouteId RouteId);
