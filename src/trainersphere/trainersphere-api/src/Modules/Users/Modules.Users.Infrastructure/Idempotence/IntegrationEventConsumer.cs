using Application.Data;
using Application.EventBus;
using MassTransit;
using Newtonsoft.Json;
using Persistence.Inbox;

namespace Modules.Users.Infrastructure.Idempotence;

/// <summary>
/// Represents the integration event consumer.
/// </summary>
/// <typeparam name="TIntegrationEvent">The integration event type.</typeparam>
internal sealed class IntegrationEventConsumer<TIntegrationEvent> : IConsumer<TIntegrationEvent>
    where TIntegrationEvent : class, IIntegrationEvent
{
    private readonly ISqlQueryExecutor _sqlQueryExecutor;

    /// <summary>
    /// Initializes a new instance of the <see cref="IntegrationEventConsumer{TIntegrationEvent}"/> class.
    /// </summary>
    /// <param name="sqlQueryExecutor">The SQL query executor.</param>
    public IntegrationEventConsumer(ISqlQueryExecutor sqlQueryExecutor) => _sqlQueryExecutor = sqlQueryExecutor;

    /// <inheritdoc />
    public async Task Consume(ConsumeContext<TIntegrationEvent> context)
    {
        TIntegrationEvent integrationEvent = context.Message;

        var inboxMessage = new InboxMessage(
            integrationEvent.Id,
            integrationEvent.OccurredOnUtc,
            integrationEvent.GetType().Name,
            JsonConvert.SerializeObject(
                integrationEvent,
                new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.All
                }));

        const string insertInboxMessageSql = @"
            INSERT INTO users.inbox_messages(id, occurred_on_utc, type, content)
            VALUES (@Id, @OccurredOnUtc, @Type, @Content::json)";

        await _sqlQueryExecutor.ExecuteAsync(insertInboxMessageSql, inboxMessage);
    }
}
