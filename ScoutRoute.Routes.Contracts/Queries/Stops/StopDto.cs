using ScoutRoute.Routes.Contracts.ValueTypes;

namespace ScoutRoute.Routes.Contracts.Queries.Stops
{
    public class StopDto
    {
        public required Guid Id { get; init; }
        public required ContactPerson ContactPerson { get; init; }
        public required string Title { get; init; }
        public required int Quantity { get; init; }
        public required string Comment { get; init; }
        public required Guid? RouteId { get; init; }
        public required decimal[] Coordinates { get; init; }
        public required int Status { get; init; }
    }
}