using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoutRoute.Payments.Contracts.Endpoints
{
    public static class PaymentEndpoints
    {
        public const string Base = "/payments";

        public const string GetIncompletePayments = $"{Base}/incomplete";
        public const string RegisterPayment = Base;
        public const string AssignAddress = $"{Base}/{{paymentId}}/address";
        public const string CompletePayment = $"{Base}/{{paymentId}}/completed";
    }
}
