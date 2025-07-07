using StronglyTypedIds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoutRoute.Routes.Domain
{
    [StronglyTypedId(Template.Guid)]

    public partial struct TeamId
    {
        public string GetStreamName() => $"Team:{Value}";
    }
}
