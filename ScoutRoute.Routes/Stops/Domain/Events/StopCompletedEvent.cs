using ScoutRoute.Routes.Domain;

namespace ScoutRoute.Routes.Stops.Domain.Events;

public record StopCompletedEvent(ProjectId ProjectId, StopId StopId);
