using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoutRoute.Routes.Contracts.Queries.Routes
{
    public class ListRouteDto
    {
        public required Guid Id { get; set; }
        public required string Name { get; set; }
    }
}
