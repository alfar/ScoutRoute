using ScoutRoute.Routes.Contracts.Queries.Stops;

namespace ScoutRoute.Routes.Contracts.Queries.Routes
{
    public class RouteDto
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }

        public required IEnumerable<StopDto> Stops { get; set; }
    }
}
