using ScoutRoute.Routes.Contracts.Queries.Stops;
using ScoutRoute.Routes.Stops.Projections;

namespace ScoutRoute.Routes.Stops.Mappings
{
    internal static class StopMappings
    {
        public static IEnumerable<StopDto> ToDtos(this IEnumerable<Stop> stops)
        {
            return stops.Select(s => s.ToDto());
        }

        public static StopDto ToDto(this Stop stop) => new StopDto() { Id = stop.Id.Value, ContactPerson = stop.ContactPerson, Title = stop.Title, Quantity = stop.Quantity, Coordinates = [stop.Latitude, stop.Longitude], Comment = stop.Comment, RouteId = stop.RouteId?.Value, Status = (int)stop.Status };
    }
}
