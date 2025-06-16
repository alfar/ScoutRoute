using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoutRoute.Routes.Contracts.Commands.Projects
{
    public class ShareProjectCommand
    {
        public required Guid NewOwnerId { get; init; }

    }
}
