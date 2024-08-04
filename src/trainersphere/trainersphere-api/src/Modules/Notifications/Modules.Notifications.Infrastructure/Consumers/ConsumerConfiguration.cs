using Infrastructure.EventBus;
using MassTransit;
using Modules.Notifications.Infrastructure.Idempotence;
using Modules.Training.IntegrationEvents;
using Modules.Users.IntegrationEvents;

namespace Modules.Notifications.Infrastructure.Consumers;

/// <summary>
/// Represents the consumer configuration for the notifications module.
/// </summary>
internal sealed class ConsumerConfiguration : IConsumerConfiguration
{
    /// <inheritdoc />
    public void AddConsumers(IRegistrationConfigurator registrationConfigurator)
    {
        registrationConfigurator.AddConsumer<IntegrationEventConsumer<UserRegisteredIntegrationEvent>>();

        registrationConfigurator.AddConsumer<IntegrationEventConsumer<InvitationSentIntegrationEvent>>();
    }
}
