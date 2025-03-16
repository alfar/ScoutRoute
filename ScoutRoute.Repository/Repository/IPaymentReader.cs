using ScoutRoute.Payments.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoutRoute.Payments.Repository
{
    public interface IPaymentReader
    {
        Task<Payment?> GetPaymentByIdAsync(PaymentId paymentId);
        Task<IReadOnlyCollection<Payment>> GetIncompletePaymentsAsync();
    }
}
