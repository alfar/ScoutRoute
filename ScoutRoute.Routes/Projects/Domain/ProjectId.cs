using StronglyTypedIds;

namespace ScoutRoute.Routes.Domain
{
    [StronglyTypedId(Template.Guid)]
    public partial struct ProjectId
    {
        public string GetStreamName() => $"Project:{Value}";
    }
}
