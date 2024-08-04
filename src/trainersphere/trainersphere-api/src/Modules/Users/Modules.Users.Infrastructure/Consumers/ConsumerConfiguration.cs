using Infrastructure.EventBus;
using MassTransit;
using Modules.Training.IntegrationEvents;
using Modules.Users.Infrastructure.Authorization.Permissions;
using Modules.Users.Infrastructure.Idempotence;

namespace Modules.Users.Infrastructure.Consumers;

/// <summary>
/// Represents the consumer configuration for the notifications module.
/// </summary>
internal sealed class ConsumerConfiguration : IConsumerConfiguration
{
    /// <inheritdoc />
    public void AddConsumers(IRegistrationConfigurator registrationConfigurator)
    {
        registrationConfigurator.AddConsumer<UserPermissionsRequestConsumer>();

        registrationConfigurator.AddConsumer<IntegrationEventConsumer<InvitationSentIntegrationEvent>>();

        registrationConfigurator.AddConsumer<IntegrationEventConsumer<InvitationCancelledIntegrationEvent>>();
    }
}
