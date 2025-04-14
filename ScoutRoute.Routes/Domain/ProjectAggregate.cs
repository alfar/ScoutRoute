
using ScoutRoute.Routes.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoutRoute.Routes.Domain
{
    internal class ProjectAggregate
    {
        private ProjectId ProjectId { get; set; }
        private string Name { get; set; } = string.Empty;
        private List<PersonId> Owners { get; } = new();

        public void HandleEvent(DomainEvent domainEvent)
        {
            switch (domainEvent)
            {
                case ProjectCreatedEvent projectCreated:
                    ProjectId = projectCreated.ProjectId;
                    Name = projectCreated.Name;
                    Owners.Add(projectCreated.OwnerId);
                    break;
                case ProjectUpdatedEvent projectUpdated:
                    Name = projectUpdated.Name;
                    break;
            }
        }

        public ProjectUpdatedEvent UpdateName(string name)
        {
            return new ProjectUpdatedEvent() { ProjectId = ProjectId, Name = name };
        }

        public static ProjectCreatedEvent Create(ProjectId id, string name, PersonId ownerId)
        {
            return new ProjectCreatedEvent() { ProjectId = id, Name = name, OwnerId = ownerId };
        }
    }
}
