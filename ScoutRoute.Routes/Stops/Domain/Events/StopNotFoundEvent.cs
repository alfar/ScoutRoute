using ScoutRoute.Routes.Domain;

namespace ScoutRoute.Routes.Stops.Domain.Events;

public record StopNotFoundEvent(ProjectId ProjectId, StopId StopId);
