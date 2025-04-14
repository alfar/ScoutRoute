using EventStore.Client;
using MongoDB.Driver;
using ScoutRoute.Routes.Contracts.Queries.Projects;
using ScoutRoute.Routes.Domain;
using ScoutRoute.Routes.Domain.Events;
using System.Text.Json;

namespace ScoutRoute.Routes.Repository
{
    internal class ProjectReader(IProjectCache projectCache) : IProjectReader
    {
        public Task<IReadOnlyCollection<ListProjectDto>> GetAllProjects()
        {
            return Task.FromResult(projectCache.GetAllProjects());
        }


        public Task<ProjectDto?> GetProjectById(ProjectId projectId)
        {
            return Task.FromResult(projectCache.GetProject(projectId));
        }
    }
}
