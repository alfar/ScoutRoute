using ScoutRoute.Payments.Domain;

namespace ScoutRoute.Payments.Repository
{
    public interface IPaymentWriter
    {
        Task SavePaymentAsync(Payment payment);
    }
}