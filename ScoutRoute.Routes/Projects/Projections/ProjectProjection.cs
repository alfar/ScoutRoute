using Marten.Events.Projections;
using ScoutRoute.Routes.Contracts.ValueTypes;
using ScoutRoute.Routes.Domain;
using ScoutRoute.Routes.Projects.Domain.Events;
using ScoutRoute.Routes.Routes.Domain.Events;
using ScoutRoute.Routes.Stops.Domain.Events;
using ScoutRoute.Shared.ValueTypes;

namespace ScoutRoute.Routes.Projects.Projections
{
    public sealed record Project(ProjectId Id, string Name, ICollection<UserId> Owners);

    public class ProjectProjection : EventProjection
    {
        public ProjectProjection()
        {
            Project<ProjectCreatedEvent>((e, ops) =>
            {
                ops.Store(new Project(e.ProjectId, e.Name, [e.OwnerId]));
            });

            ProjectAsync<ProjectUpdatedEvent>(async (e, ops) =>
            {
                var project = await ops.LoadAsync<Project>(e.ProjectId);
                if (project is not null)
                {
                    ops.Store(project with { Name = e.Name });
                }
            });

            ProjectAsync<ProjectSharedEvent>(async (e, ops) =>
            {
                var project = await ops.LoadAsync<Project>(e.ProjectId);
                if (project is not null)
                {
                    ops.Store(project with { Owners = [.. project.Owners, e.NewOwnerId] });
                }
            });

        }
    }
}
