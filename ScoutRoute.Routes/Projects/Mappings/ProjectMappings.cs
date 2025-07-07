using ScoutRoute.Routes.Contracts.Queries.Projects;
using ScoutRoute.Routes.Projects.Projections;

namespace ScoutRoute.Routes.Projects.Mappings
{
    internal static class ProjectMappings
    {
        public static IEnumerable<ListProjectDto> ToDtos(this IEnumerable<ListProject> projects)
        {
            return projects.Select(p => p.ToDto());
        }


        public static ListProjectDto ToDto(this ListProject project)
        {
            return new() { Id = project.Id.Value, Name = project.Name };
        }

        public static ProjectDto ToDto(this Project project)
        {
            return new ProjectDto() { Id = project.Id.Value, Name = project.Name, OwnerIds = project.Owners.Select(o => o.Value) };
        }
    }
}
