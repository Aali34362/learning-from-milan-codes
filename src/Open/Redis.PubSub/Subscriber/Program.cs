using Subscriber;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<Consumer>();
    })
    .Build();

host.Run();
