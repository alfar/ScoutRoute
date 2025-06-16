using ScoutRoute.Routes.Domain;

namespace ScoutRoute.Routes.Teams.Domain.Events
{
    public sealed record TeamNameUpdatedEvent(ProjectId ProjectId, TeamId TeamId, string Name);
}
