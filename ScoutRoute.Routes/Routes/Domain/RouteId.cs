using StronglyTypedIds;

namespace ScoutRoute.Routes.Domain
{
    [StronglyTypedId(Template.Guid)]

    public partial struct RouteId
    {
        public string GetStreamName() => $"Route:{Value}";
    }
}
