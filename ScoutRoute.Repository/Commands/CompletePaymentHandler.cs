using MediatR;
using ScoutRoute.Payments.Domain;
using ScoutRoute.Payments.Repository;
using SharpCompress.Writers;

namespace ScoutRoute.Payments.Commands
{
    public class CompletePaymentRequest : IRequest<CompletePaymentResult>
    {
        private CompletePaymentRequest(PaymentId id)
        {
            Id = id;
        }

        public PaymentId Id { get; init; }

        public static CompletePaymentRequest Create(PaymentId id)
        {
            return new CompletePaymentRequest(id);
        }
    }

    public record CompletePaymentResult(bool Success);

    internal class CompletePaymentRequestHandler(IPaymentReader reader, IPaymentWriter writer) : IRequestHandler<CompletePaymentRequest, CompletePaymentResult>
    {
        public async Task<CompletePaymentResult> Handle(CompletePaymentRequest request, CancellationToken cancellationToken)
        {
            var payment = await reader.GetPaymentByIdAsync(request.Id);

            if (payment is null)
                return new(false);

            payment.Complete();

            await writer.SavePaymentAsync(payment);

            return new(true);
        }
    }
}
