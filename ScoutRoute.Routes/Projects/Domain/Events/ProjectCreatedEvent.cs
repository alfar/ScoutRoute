using ScoutRoute.Routes.Domain;
using ScoutRoute.Shared.ValueTypes;

namespace ScoutRoute.Routes.Projects.Domain.Events
{
    public sealed record ProjectCreatedEvent(ProjectId ProjectId, string Name, UserId OwnerId);
}
