namespace ScoutRoute.Routes.Contracts.Commands.Stops
{
    public class AssignRouteCommand
    {
        public required Guid RouteId { get; init; }
    }
}
