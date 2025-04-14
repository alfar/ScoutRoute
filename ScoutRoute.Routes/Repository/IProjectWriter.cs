using ScoutRoute.Routes.Domain;

namespace ScoutRoute.Routes.Repository
{
    internal interface IProjectWriter
    {
        Task<bool> CreateProjectAsync(ProjectId id, string name, PersonId ownerId);
        Task<bool> UpdateProjectAsync(ProjectId projectId, string name);
    }
}