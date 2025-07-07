using ScoutRoute.Routes.Domain;

namespace ScoutRoute.Routes.Teams.Domain.Events
{
    public sealed record TeamMemberAddedEvent(ProjectId ProjectId, TeamId TeamId, string MemberName);
}
