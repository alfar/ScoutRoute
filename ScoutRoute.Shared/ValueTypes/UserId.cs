using Microsoft.AspNetCore.Http;
using StronglyTypedIds;
using System.Security.Claims;

namespace ScoutRoute.Shared.ValueTypes
{
    [StronglyTypedId(Template.Guid)]
    public partial struct UserId
    {
        public static ValueTask<UserId> BindAsync(HttpContext context)
        {
            var ownerId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            return ValueTask.FromResult(UserId.Parse(ownerId!));
        }
    }
}
