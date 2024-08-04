using MassTransit;

namespace Infrastructure.EventBus;

/// <summary>
/// Represents the event bus consumer configuration interface.
/// </summary>
public interface IConsumerConfiguration
{
    /// <summary>
    /// Adds the consumer definitions using the specified registration configurator.
    /// </summary>
    /// <param name="registrationConfigurator">The registration configurator.</param>
    void AddConsumers(IRegistrationConfigurator registrationConfigurator);
}
