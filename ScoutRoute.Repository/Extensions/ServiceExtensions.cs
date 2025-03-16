using Microsoft.Extensions.DependencyInjection;
using ScoutRoute.Payments.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoutRoute.Payments.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddPaymentServices(this IServiceCollection services)
        {
            services.AddScoped<IPaymentWriter, PaymentWriter>();
            services.AddScoped<IPaymentReader, PaymentReader>();

            return services;
        }
    }
}
