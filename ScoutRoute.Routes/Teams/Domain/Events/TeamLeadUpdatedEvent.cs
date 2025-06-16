using ScoutRoute.Routes.Domain;

namespace ScoutRoute.Routes.Teams.Domain.Events
{
    public sealed record TeamLeadUpdatedEvent(ProjectId ProjectId, TeamId TeamId, string TeamLead);
}
