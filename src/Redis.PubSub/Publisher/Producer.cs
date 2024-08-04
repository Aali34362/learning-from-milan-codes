using System.Text.Json;
using Contracts;
using StackExchange.Redis;

namespace Publisher;

public class Producer : BackgroundService
{
    private static readonly string ConnectionString = "localhost:6379";
    private static readonly ConnectionMultiplexer Connection = ConnectionMultiplexer.Connect(ConnectionString);
    private const string Channel = "test-channel";

    private readonly ILogger<Producer> _logger;

    public Producer(ILogger<Producer> logger)
    {
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var subscriber = Connection.GetSubscriber();

        while (!stoppingToken.IsCancellationRequested)
        {
            var message = new Message(Guid.NewGuid(), DateTime.UtcNow);

            var json = JsonSerializer.Serialize(message);

            await subscriber.PublishAsync(Channel, json);

            _logger.LogInformation("Sending message: {@Message}", message);

            await Task.Delay(5000, stoppingToken);
        }
    }
}
