using Microsoft.AspNetCore.Http;
using ScoutRoute.Routes.Domain;
using ScoutRoute.Shared.ValueTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ScoutRoute.Routes.Extensions
{
    internal static class UserExtensions
    {
        public static async Task<IResult> LoggedIn(this ClaimsPrincipal user, Func<UserId, Task<IResult>> next)
        {
            if (user == null) return Results.Unauthorized();

            var ownerId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (ownerId is null) return Results.Unauthorized();

            return await next(UserId.Parse(ownerId));
        }
    }
}
