using System.Reflection;
using Infrastructure.Configuration;
using Infrastructure.EventBus;
using MassTransit;
using Shared.Extensions;

namespace Infrastructure.Extensions;

/// <summary>
/// Contains extension methods for the <see cref="IRegistrationConfigurator"/> interface.
/// </summary>
public static class RegistrationConfiguratorExtensions
{
    /// <summary>
    /// Adds the consumers defined in the specified assemblies.
    /// </summary>
    /// <param name="registrationConfigurator">The registration configurator.</param>
    /// <param name="assemblies">The assemblies to scan for consumers.</param>
    public static void AddConsumersFromAssemblies(this IRegistrationConfigurator registrationConfigurator, params Assembly[] assemblies) =>
        InstanceFactory
            .CreateFromAssemblies<IConsumerConfiguration>(assemblies)
            .ForEach(consumerInstaller => consumerInstaller.AddConsumers(registrationConfigurator));

    /// <summary>
    /// Adds the request clients defined in the specified assemblies.
    /// </summary>
    /// <param name="registrationConfigurator">The registration configurator.</param>
    /// <param name="assemblies">The assemblies to scan for request clients.</param>
    public static void AddRequestClientsFromAssemblies(this IRegistrationConfigurator registrationConfigurator, params Assembly[] assemblies) =>
        InstanceFactory
            .CreateFromAssemblies<IRequestClientConfiguration>(assemblies)
            .ForEach(consumerInstaller => consumerInstaller.AddRequestClients(registrationConfigurator));
}
