using ScoutRoute.Routes.Domain;

namespace ScoutRoute.Routes.Teams.Domain.Events
{
    public sealed record TeamMemberRemovedEvent(ProjectId ProjectId, TeamId TeamId, string MemberName);
}
