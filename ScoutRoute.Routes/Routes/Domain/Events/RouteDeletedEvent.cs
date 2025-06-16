using ScoutRoute.Routes.Domain;

namespace ScoutRoute.Routes.Routes.Domain.Events;

public record RouteDeletedEvent(ProjectId ProjectId, RouteId RouteId);
