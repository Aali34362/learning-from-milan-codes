using MassTransit;

namespace Infrastructure.EventBus;

/// <summary>
/// Represents the event bus request client configuration interface.
/// </summary>
public interface IRequestClientConfiguration
{
    /// <summary>
    /// Adds the request client definitions using the specified registration configurator.
    /// </summary>
    /// <param name="registrationConfigurator">The registration configurator.</param>
    void AddRequestClients(IRegistrationConfigurator registrationConfigurator);
}
