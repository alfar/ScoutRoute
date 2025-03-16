using MediatR;
using ScoutRoute.Payments.Commands;
using ScoutRoute.Payments.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoutRoute.Payments.Mapping.Commands
{
    internal static class RegisterPaymentCommandExtensions
    {
        public static Payment ToPayment(this RegisterPaymentCommand command)
        {
            return Payment.Create(command.Id, command.Message, command.Amount, command.Received);
        }
    }
}
