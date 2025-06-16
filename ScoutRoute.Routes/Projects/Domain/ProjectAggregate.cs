using ScoutRoute.Routes.Contracts.ValueTypes;
using ScoutRoute.Routes.Domain;
using ScoutRoute.Routes.Projects.Domain.Events;
using ScoutRoute.Routes.Routes.Domain;
using ScoutRoute.Routes.Routes.Domain.Events;
using ScoutRoute.Routes.Stops.Domain;
using ScoutRoute.Routes.Stops.Domain.Events;
using ScoutRoute.Shared.ValueTypes;

namespace ScoutRoute.Routes.Projects.Domain
{
    public class ProjectAggregate
    {
        public string Id { get; set; } = "";

        private ProjectId ProjectId { get; set; }
        private string Name { get; set; } = string.Empty;
        private List<UserId> Owners { get; } = new();

        public void Apply(ProjectCreatedEvent @event)
        {
            ProjectId = @event.ProjectId;
            Name = @event.Name;
            Owners.Add(@event.OwnerId);
        }

        public void Apply(ProjectUpdatedEvent @event)
        {
            Name = @event.Name;
        }

        public void Apply(ProjectSharedEvent @event)
        {
            Owners.Add(@event.NewOwnerId);
        }

        public ProjectUpdatedEvent UpdateName(string name, UserId ownerId)
        {
            if (!Owners.Contains(ownerId))
            {
                throw new ArgumentException("Invalid owner");
            }
            return new ProjectUpdatedEvent(ProjectId, name, ownerId);
        }

        public static ProjectCreatedEvent CreateProject(ProjectId id, string name, UserId ownerId)
        {
            return new ProjectCreatedEvent(id, name, ownerId);
        }

        public ProjectSharedEvent AddOwner(UserId newOwnerId, UserId ownerId)
        {
            return new ProjectSharedEvent(ProjectId, newOwnerId, ownerId);
        }
    }
}
