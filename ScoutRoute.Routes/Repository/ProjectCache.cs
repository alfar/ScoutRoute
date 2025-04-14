using ScoutRoute.Routes.Contracts.Queries.Projects;
using ScoutRoute.Routes.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoutRoute.Routes.Repository
{
    public class ProjectCache : IProjectCache
    {
        private readonly Dictionary<ProjectId, ListProjectDto> _listCache = new();
        private readonly Dictionary<ProjectId, ProjectDto> _cache = new();

        public void CacheListProject(ProjectId id, string name)
        {
            _listCache[id] = new ListProjectDto() { Id = id.Value, Name = name };
        }

        public void CacheProject(ProjectDto project)
        {
            _cache[new ProjectId(project.Id)] = project;
        }

        public IReadOnlyCollection<ListProjectDto> GetAllProjects()
        {
            return _listCache.Values;
        }

        public ProjectDto? GetProject(ProjectId projectId)
        {
            return _cache.TryGetValue(projectId, out var project) ? project : null;
        }
    }
}
