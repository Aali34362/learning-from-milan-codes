using Application.EventBus;
using Application.ServiceLifetimes;
using MassTransit;

namespace Infrastructure.EventBus;

/// <summary>
/// Represents the event bus.
/// </summary>
public sealed class EventBus : IEventBus, ITransient
{
    private readonly IBus _bus;

    /// <summary>
    /// Initializes a new instance of the <see cref="EventBus"/> class.
    /// </summary>
    /// <param name="bus">The bus.</param>
    public EventBus(IBus bus) => _bus = bus;

    /// <inheritdoc />
    public async Task PublishAsync<TIntegrationEvent>(TIntegrationEvent integrationEvent, CancellationToken cancellationToken = default)
        where TIntegrationEvent : IIntegrationEvent =>
        await _bus.Publish(integrationEvent, cancellationToken);
}
