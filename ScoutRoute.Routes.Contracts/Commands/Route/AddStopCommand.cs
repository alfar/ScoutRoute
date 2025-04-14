using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoutRoute.Routes.Contracts.Commands.Route
{
    public class AddStopCommand
    {
        public required Guid StopId { get; init; }
    }
}
