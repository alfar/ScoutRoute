using ScoutRoute.Routes.Contracts.ValueTypes;
using ScoutRoute.Routes.Domain;
using ScoutRoute.Routes.Stops.Domain.Events;
using ScoutRoute.Shared.ValueTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoutRoute.Routes.Stops.Domain
{
    public class StopAggregate
    {
        public void Apply(StopCreatedEvent @event)
        {
            ProjectId = @event.ProjectId;
            StopId = @event.StopId;
            ContactPerson = @event.ContactPerson;
            Title = @event.Title;
            Quantity = @event.Quantity;
            Latitude = @event.Latitude;
            Longitude = @event.Longitude;
            Comment = @event.Comment;
        }

        public string Id { get; set; } = "";

        private ProjectId ProjectId { get; set; }
        private StopId StopId { get; set; }
        private ContactPerson? ContactPerson { get; set; }
        private string Title { get; set; } = "";
        private int Quantity { get; set; } = 1;

        private decimal Latitude { get; set; }
        private decimal Longitude { get; set; }

        private string Comment { get; set; } = "";
        private RouteId? RouteId { get; set; }

        public static StopCreatedEvent Create(ProjectId projectId, StopId stopId, ContactPerson contactPerson, string title, int quantity, decimal latitude, decimal longitude, string comment, UserId userId)
        {
            return new StopCreatedEvent(projectId, stopId, contactPerson, title, quantity, latitude, longitude, comment, userId);
        }

        public StopAddedToRouteEvent AddToRoute(RouteId routeId)
        {
            return new StopAddedToRouteEvent(ProjectId, StopId, routeId);
        }

        public StopDeletedEvent Delete()
        {
            return new StopDeletedEvent(ProjectId, StopId);
        }
    }
}
