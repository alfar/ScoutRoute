using JasperFx.Core;
using ScoutRoute.Routes.Domain;
using ScoutRoute.Routes.Routes.Domain.Events;

namespace ScoutRoute.Routes.Routes.Domain;

public class RouteAggregate
{
    public string Id { get; set; } = "";

    private ProjectId ProjectId { get; set; }
    private RouteId RouteId { get; set; }
    private string Name { get; set; } = "";
    private StopId[] Stops { get; set; } = [];

    private TeamId? AssignedTeamId { get; set; }

    private int Status { get; set; }

    private bool Deleted { get; set; }

    private void EnsureNotDeleted()
    {
        if (Deleted)
            throw new InvalidOperationException($"Route {RouteId} is deleted.");
    }

    public static RouteCreatedEvent CreateRoute(
        ProjectId projectId,
        RouteId routeId,
        string name,
        IEnumerable<StopId> stops
    )
    {
        // Static method, no instance to check
        return new RouteCreatedEvent(projectId, routeId, name, stops.ToArray());
    }

    public RouteAssignedToTeamEvent AssignTeam(TeamId teamId)
    {
        EnsureNotDeleted();
        return new(ProjectId, RouteId, teamId);
    }

    public RouteUnassignedEvent UnassignTeam()
    {
        EnsureNotDeleted();
        if (AssignedTeamId == null)
        {
            throw new InvalidOperationException($"Route {RouteId} is not assigned to any team.");
        }

        return new(ProjectId, RouteId);
    }

    public RouteStopAddedEvent AddStop(StopId stopId)
    {
        EnsureNotDeleted();
        if (Stops.Contains(stopId))
        {
            throw new InvalidOperationException(
                $"Stop {stopId} is already assigned to route {RouteId}."
            );
        }
        return new RouteStopAddedEvent(ProjectId, RouteId, stopId);
    }

    public RouteStopRemovedEvent RemoveStop(StopId stopId)
    {
        EnsureNotDeleted();
        if (!Stops.Contains(stopId))
        {
            throw new InvalidOperationException(
                $"Stop {stopId} is not assigned to route {RouteId}."
            );
        }
        return new RouteStopRemovedEvent(ProjectId, RouteId, stopId);
    }

    public RouteUpdatedEvent UpdateName(string name)
    {
        EnsureNotDeleted();
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Route name cannot be empty.", nameof(name));
        }
        return new RouteUpdatedEvent(ProjectId, RouteId, name);
    }

    public RouteCommentAddedEvent AddComment(string comment)
    {
        EnsureNotDeleted();
        if (string.IsNullOrWhiteSpace(comment))
            throw new ArgumentException("Comment cannot be empty.", nameof(comment));
        return new RouteCommentAddedEvent(ProjectId, RouteId, comment);
    }

    public RouteCompletedEvent MarkComplete()
    {
        EnsureNotDeleted();
        return new RouteCompletedEvent(ProjectId, RouteId);
    }

    public RouteOverfilledEvent MarkOverfilled()
    {
        EnsureNotDeleted();
        return new RouteOverfilledEvent(ProjectId, RouteId);
    }

    public RouteStatusResetEvent ResetStatus()
    {
        return new RouteStatusResetEvent(ProjectId, RouteId);
    }

    public RouteExtraStopsChangedEvent ChangeExtraStops(int extraStops)
    {
        EnsureNotDeleted();
        return new RouteExtraStopsChangedEvent(ProjectId, RouteId, extraStops);
    }

    public RouteDeletedEvent Delete()
    {
        EnsureNotDeleted();
        return new RouteDeletedEvent(ProjectId, RouteId);
    }

    public void Apply(RouteCreatedEvent @event)
    {
        ProjectId = @event.ProjectId;
        RouteId = @event.RouteId;
        Name = @event.Name;
        Stops = @event.Stops;
    }

    public void Apply(RouteUpdatedEvent @event)
    {
        Name = @event.Name;
    }

    public void Apply(RouteAssignedToTeamEvent @event)
    {
        AssignedTeamId = @event.TeamId;
    }

    public void Apply(RouteUnassignedEvent @event)
    {
        AssignedTeamId = null;
    }

    public void Apply(RouteStopAddedEvent @event)
    {
        if (Stops.Contains(@event.StopId))
        {
            throw new InvalidOperationException(
                $"Stop {@event.StopId} is already assigned to route {RouteId}."
            );
        }

        Stops = [.. Stops, @event.StopId];
    }

    public void Apply(RouteCompletedEvent @event)
    {
        Status = 1;
    }

    public void Apply(RouteOverfilledEvent @event)
    {
        Status = 2;
    }

    public void Apply(RouteStatusResetEvent @event)
    {
        Status = 0;
    }

    public void Apply(RouteDeletedEvent @event)
    {
        Deleted = true;
    }
}
