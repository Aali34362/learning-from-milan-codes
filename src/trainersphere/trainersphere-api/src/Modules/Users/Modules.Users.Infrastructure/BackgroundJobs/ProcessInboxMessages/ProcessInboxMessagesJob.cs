using Application.Data;
using Application.EventBus;
using Application.Time;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Polly;
using Polly.Retry;
using Quartz;

namespace Modules.Users.Infrastructure.BackgroundJobs.ProcessInboxMessages;

/// <summary>
/// Represents the background job for processing inbox messages.
/// </summary>
[DisallowConcurrentExecution]
internal sealed class ProcessInboxMessagesJob : IJob
{
    private static readonly JsonSerializerSettings JsonSerializerSettings = new()
    {
        TypeNameHandling = TypeNameHandling.All
    };

    private readonly ISqlQueryExecutor _sqlQueryExecutor;
    private readonly ISystemTime _systemTime;
    private readonly IServiceProvider _serviceProvider;
    private readonly ProcessInboxMessagesOptions _options;
    private readonly AsyncRetryPolicy _policy;

    /// <summary>
    /// Initializes a new instance of the <see cref="ProcessInboxMessagesJob"/> class.
    /// </summary>
    /// <param name="sqlQueryExecutor">The SQL query executor.</param>
    /// <param name="systemTime">The system time.</param>
    /// <param name="serviceProvider">The service provider.</param>
    /// <param name="options">The options.</param>
    public ProcessInboxMessagesJob(
        ISqlQueryExecutor sqlQueryExecutor,
        ISystemTime systemTime,
        IServiceProvider serviceProvider,
        IOptions<ProcessInboxMessagesOptions> options)
    {
        _sqlQueryExecutor = sqlQueryExecutor;
        _systemTime = systemTime;
        _serviceProvider = serviceProvider;
        _options = options.Value;
        _policy = Policy.Handle<Exception>().RetryAsync(_options.RetryCount);
    }

    /// <inheritdoc />
    public async Task Execute(IJobExecutionContext context)
    {
        List<InboxMessageResponse> inboxMessagesList = await GetInboxMessagesAsync();

        if (inboxMessagesList.Count == 0)
        {
            return;
        }

        foreach (InboxMessageResponse inboxMessage in inboxMessagesList)
        {
            IIntegrationEvent integrationEvent = JsonConvert.DeserializeObject<IIntegrationEvent>(inboxMessage.Content, JsonSerializerSettings)!;

            IEnumerable<IIntegrationEventHandler> handlers = IntegrationEventHandlerFactory.GetHandlers(
                integrationEvent.GetType(),
                _serviceProvider);

            PolicyResult result = await _policy.ExecuteAndCaptureAsync(async () =>
            {
                foreach (IIntegrationEventHandler handler in handlers)
                {
                    await handler.Handle(integrationEvent, context.CancellationToken);
                }
            });

            await UpdateInboxMessageAsync(inboxMessage, result.FinalException);
        }
    }

    private async Task<List<InboxMessageResponse>> GetInboxMessagesAsync()
    {
        string getInboxMessagesSql = $@"
            SELECT id, content
            FROM users.inbox_messages
            WHERE processed_on_utc IS NULL
            ORDER BY occurred_on_utc
            LIMIT {_options.BatchSize}";

        IEnumerable<InboxMessageResponse> inboxMessages = await _sqlQueryExecutor.QueryAsync<InboxMessageResponse>(getInboxMessagesSql);

        return inboxMessages.ToList();
    }

    private async Task UpdateInboxMessageAsync(InboxMessageResponse inboxMessage, Exception? exception)
    {
        const string updateInboxMessageSql = @"
            UPDATE users.inbox_messages
            SET processed_on_utc = @ProcessedOnUtc,
                error = @Error
            WHERE id = @Id";

        await _sqlQueryExecutor.ExecuteAsync(
            updateInboxMessageSql,
            new
            {
                inboxMessage.Id,
                ProcessedOnUtc = _systemTime.UtcNow,
                Error = exception?.ToString()
            });
    }

    internal sealed record InboxMessageResponse(Guid Id, string Content);
}
