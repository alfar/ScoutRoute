using ScoutRoute.Routes.Contracts.Queries.Projects;
using ScoutRoute.Routes.Domain;

namespace ScoutRoute.Routes.Mapping.Queries
{
    internal static class ProjectMappings
    {
        public static ProjectQueryResult ToResult(this IEnumerable<Project> projects)
        {
            return new ProjectQueryResult() { Projects = projects.Select(p => p.ToListDto()) };
        }

        public static ListProjectDto ToListDto(this Project project)
        {
            return new ListProjectDto() { Id = project.Id.Value, Name = project.Name };
        }

        public static ProjectDto ToDto(this Project project)
        {
            return new ProjectDto() { Id = project.Id.Value, Name = project.Name, OwnerIds = project.Owners.Select(o => o.Value) };
        }
    }
}
