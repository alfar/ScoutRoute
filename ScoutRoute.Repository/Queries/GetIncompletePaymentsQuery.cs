using MediatR;
using ScoutRoute.Payments.Contracts.Queries;
using ScoutRoute.Payments.Mapping.Commands;
using ScoutRoute.Payments.Repository;

namespace ScoutRoute.Payments.Queries
{
    public class GetIncompletePaymentsQuery : IRequest<GetIncompletePaymentsQueryResult>
    {
    }

    public class GetIncompletePaymentsQueryHandler : IRequestHandler<GetIncompletePaymentsQuery, GetIncompletePaymentsQueryResult>
    {
        private readonly IPaymentReader _paymentReader;

        public GetIncompletePaymentsQueryHandler(IPaymentReader paymentReader)
        {
            _paymentReader = paymentReader;
        }

        public async Task<GetIncompletePaymentsQueryResult> Handle(GetIncompletePaymentsQuery request, CancellationToken cancellationToken)
        {
            var payments = await _paymentReader.GetIncompletePaymentsAsync();

            return new GetIncompletePaymentsQueryResult(payments.ToDtos());
        }
    }
}
