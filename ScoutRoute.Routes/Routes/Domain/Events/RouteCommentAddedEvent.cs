using ScoutRoute.Routes.Domain;

namespace ScoutRoute.Routes.Routes.Domain.Events;

public sealed record RouteCommentAddedEvent(ProjectId ProjectId, RouteId RouteId, string Comment);
