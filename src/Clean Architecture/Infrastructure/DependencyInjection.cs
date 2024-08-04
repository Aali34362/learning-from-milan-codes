using Application.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddTransient<IEmailService, EmailService>();

        services.AddTransient<IStockService, StockService>();

        services.AddTransient<IPaymentService, PaymentService>();

        return services;
    }
}
