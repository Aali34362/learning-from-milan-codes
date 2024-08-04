using Authorization.Contracts;
using Infrastructure.EventBus;
using MassTransit;

namespace Authorization.RequestClients;

/// <summary>
/// Represents the request client configuration.
/// </summary>
internal sealed class RequestClientConfiguration : IRequestClientConfiguration
{
    /// <inheritdoc />
    public void AddRequestClients(IRegistrationConfigurator registrationConfigurator) =>
        registrationConfigurator.AddRequestClient<IUserPermissionsRequest>();
}
