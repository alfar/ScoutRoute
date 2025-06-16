using ScoutRoute.Routes.Contracts.Queries.Routes;
using ScoutRoute.Routes.Routes.Projections;
using ScoutRoute.Routes.Stops.Mappings;
using ScoutRoute.Routes.Stops.Projections;

namespace ScoutRoute.Routes.Routes.Mappings
{
    internal static class RouteMappings
    {
        public static IEnumerable<ListRouteDto> ToListDtos(this IEnumerable<Route> routes)
        {
            return routes.Select(r => r.ToListDto());
        }

        public static ListRouteDto ToListDto(this Route route)
        {
            return new() { Id = route.Id.Value, Name = route.Name };
        }

        public static RouteDto ToDto(this Route route, IEnumerable<Stop> stops)
        {
            return new() { Id = route.Id.Value, Name = route.Name, Stops = stops.ToDtos() };
        }
    }
}
