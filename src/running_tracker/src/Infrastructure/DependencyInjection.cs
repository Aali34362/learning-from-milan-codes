using Application.Abstractions.Data;
using Application.Abstractions.Notifications;
using Domain.Followers;
using Domain.Users;
using Infrastructure.Data;
using Infrastructure.Notifications;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedKernel;

namespace Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

        string? connectionString = configuration.GetConnectionString("Database");
        Ensure.NotNullOrEmpty(connectionString);

        services.AddTransient<IDbConnectionFactory>(
            _ => new DbConnectionFactory(connectionString));

        services.AddDbContext<IApplicationDbContext, ApplicationDbContext>(
            options => options.UseSqlServer(connectionString));

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IFollowerRepository, FollowerRepository>();

        services.AddTransient<INotificationService, NotificationService>();
    }
}
