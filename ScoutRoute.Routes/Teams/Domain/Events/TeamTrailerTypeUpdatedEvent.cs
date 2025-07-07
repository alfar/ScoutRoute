using ScoutRoute.Routes.Domain;

namespace ScoutRoute.Routes.Teams.Domain.Events
{
    public sealed record TeamTrailerTypeUpdatedEvent(ProjectId ProjectId, TeamId TeamId, TrailerType TrailerType);
}
