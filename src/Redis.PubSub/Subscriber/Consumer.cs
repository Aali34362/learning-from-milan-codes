using System.Text.Json;
using StackExchange.Redis;
using System.Threading.Channels;
using Contracts;

namespace Subscriber;

public class Consumer : BackgroundService
{
    private static readonly string ConnectionString = "localhost:6379";
    private static readonly ConnectionMultiplexer Connection = ConnectionMultiplexer.Connect(ConnectionString);
    private const string Channel = "test-channel";

    private readonly ILogger<Consumer> _logger;

    public Consumer(ILogger<Consumer> logger)
    {
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var subscriber = Connection.GetSubscriber();

        await subscriber.SubscribeAsync(Channel, (channel, message) =>
        {
            var messageDeserialized = JsonSerializer.Deserialize<Message>(message);

            _logger.LogInformation(
                "Received message: {Channel} {@Message}",
                channel,
                messageDeserialized);
        });
    }
}
