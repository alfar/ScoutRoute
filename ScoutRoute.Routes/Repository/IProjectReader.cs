using ScoutRoute.Routes.Contracts.Queries.Projects;
using ScoutRoute.Routes.Domain;

namespace ScoutRoute.Routes.Repository
{
    internal interface IProjectReader
    {
        Task<IReadOnlyCollection<ListProjectDto>> GetAllProjects();
        Task<ProjectDto?> GetProjectById(ProjectId projectId);
    }
}