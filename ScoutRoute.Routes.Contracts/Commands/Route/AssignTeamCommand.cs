using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoutRoute.Routes.Contracts.Commands.Route
{
    public class AssignTeamCommand
    {
        public required Guid TeamId { get; init; }
    }
}
