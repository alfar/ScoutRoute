using ScoutRoute.Routes.Domain;

namespace ScoutRoute.Routes.Teams.Domain.Events
{
    public sealed record TeamPhoneUpdatedEvent(ProjectId ProjectId, TeamId TeamId, string Phone);
}
