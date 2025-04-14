namespace ScoutRoute.Routes.Contracts.Commands.Route
{
    public class CreateRouteCommand
    {
        public required Guid Id { get; init; }
        public required string Name { get; init; }
        public required IEnumerable<Guid> Stops { get; init; }
    }
}
