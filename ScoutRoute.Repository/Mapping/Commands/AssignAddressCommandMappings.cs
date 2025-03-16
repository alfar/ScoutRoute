using ScoutRoute.Payments.Commands;
using ScoutRoute.Payments.Domain;
using ScoutRoute.Payments.Endpoints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoutRoute.Payments.Mapping.Commands
{
    internal static class AssignAddressCommandMappings
    {
        public static AssignAddressCommand ToCommand(this AssignAddressDto dto, PaymentId paymentId)
        {
            return new AssignAddressCommand() { AddressId = dto.AddressId, PaymentId = paymentId };
        }
    }
}
