using ScoutRoute.Routes.Contracts.Queries.Projects;
using ScoutRoute.Routes.Domain;

namespace ScoutRoute.Routes.Repository
{
    public interface IProjectCache
    {
        void CacheListProject(ProjectId id, string name);
        void CacheProject(ProjectDto project);

        IReadOnlyCollection<ListProjectDto> GetAllProjects();
        ProjectDto? GetProject(ProjectId projectId);
    }
}