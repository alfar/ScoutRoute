namespace ScoutRoute.Routes.Contracts.Commands.Projects
{
    public class CreateProjectCommand
    {
        public required Guid Id { get; init; }
        public required string Name { get; init; }
    }
}
