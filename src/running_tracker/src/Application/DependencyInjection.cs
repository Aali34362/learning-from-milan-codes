using Application.Abstractions.Behaviors;
using Domain.Followers;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);

            config.AddOpenBehavior(typeof(QueryCachingPipelineBehavior<,>));
        });

        services.AddScoped<IFollowerService, FollowerService>();

        return services;
    }
}
