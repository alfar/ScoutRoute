using ScoutRoute.Routes.Domain;

namespace ScoutRoute.Routes.Routes.Domain.Events;

public record RouteUpdatedEvent(ProjectId ProjectId, RouteId RouteId, string Name);
