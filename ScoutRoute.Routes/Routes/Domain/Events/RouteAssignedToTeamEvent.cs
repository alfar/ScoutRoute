using ScoutRoute.Routes.Domain;

namespace ScoutRoute.Routes.Routes.Domain.Events;

public sealed record RouteAssignedToTeamEvent(ProjectId ProjectId, RouteId RouteId, TeamId TeamId);
