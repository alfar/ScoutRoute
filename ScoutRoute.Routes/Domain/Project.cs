using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoutRoute.Routes.Domain
{
    internal class Project
    {
        public required ProjectId Id { get; init; }
        public required string Name { get; init; }
        public ICollection<PersonId> Owners { get; init; } = new List<PersonId>();
    }
}
