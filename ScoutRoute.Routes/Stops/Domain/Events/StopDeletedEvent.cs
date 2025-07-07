using ScoutRoute.Routes.Domain;

namespace ScoutRoute.Routes.Stops.Domain.Events;

public sealed record StopDeletedEvent(ProjectId ProjectId, StopId StopId);
