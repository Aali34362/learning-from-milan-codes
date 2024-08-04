using Infrastructure.Configuration;
using Infrastructure.Extensions;
using MassTransit;

namespace App.ServiceInstallers.EventBus;

/// <summary>
/// Represents the event bus service installer.
/// </summary>
internal sealed class EventBusServiceInstaller : IServiceInstaller
{
    /// <inheritdoc />
    public void Install(IServiceCollection services, IConfiguration configuration) =>
        services
            .ConfigureOptions<MassTransitHostOptionsSetup>()
            .AddMassTransit(bussConfigurator =>
            {
                bussConfigurator.SetKebabCaseEndpointNameFormatter();

                bussConfigurator.AddConsumersFromAssemblies(
                    Modules.Users.Infrastructure.AssemblyReference.Assembly,
                    Modules.Training.Infrastructure.AssemblyReference.Assembly,
                    Modules.Notifications.Infrastructure.AssemblyReference.Assembly);

                bussConfigurator.AddRequestClientsFromAssemblies(
                    Authorization.AssemblyReference.Assembly);

                bussConfigurator.UsingInMemory((context, configurator) => configurator.ConfigureEndpoints(context));
            });
}
