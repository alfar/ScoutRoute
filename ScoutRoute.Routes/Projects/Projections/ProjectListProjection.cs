using Marten.Events.Projections;
using ScoutRoute.Routes.Domain;
using ScoutRoute.Routes.Projects.Domain.Events;
using ScoutRoute.Shared.ValueTypes;

namespace ScoutRoute.Routes.Projects.Projections
{
    public sealed record ListProject(ProjectId Id, string Name);
    public sealed record ProjectOwner(UserId Id, ICollection<ListProject> Projects);

    public class ProjectListProjection : EventProjection
    {
        public ProjectListProjection()
        {
            ProjectAsync<ProjectCreatedEvent>(async (e, ops) =>
            {
                var owner = await ops.LoadAsync<ProjectOwner>(e.OwnerId);

                if (owner is not null)
                {
                    owner.Projects.Add(new ListProject(e.ProjectId, e.Name));
                }
                else
                {
                    owner = new ProjectOwner(e.OwnerId, [new ListProject(e.ProjectId, e.Name)]);
                }

                ops.Store(owner);
            });

            ProjectAsync<ProjectSharedEvent>(async (e, ops) =>
            {
                var owner = await ops.LoadAsync<ProjectOwner>(e.OwnerId);

                if (owner is null) return;

                var addedProject = owner.Projects.FirstOrDefault(p => p.Id == e.ProjectId);

                if (addedProject is null) return;

                var newOwner = await ops.LoadAsync<ProjectOwner>(e.NewOwnerId);

                if (newOwner is not null)
                {
                    newOwner.Projects.Add(addedProject);
                }
                else
                {
                    newOwner = new ProjectOwner(e.OwnerId, [addedProject]);
                }

                ops.Store(newOwner);
            });
        }
    }
}
