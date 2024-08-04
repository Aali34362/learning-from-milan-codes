using Application;
using Application.Abstractions.EventBus;
using Application.Behaviors;
using Application.Products.CreateProduct;
using Carter;
using Infrastructure.MessageBroker;
using Infrastructure.Outbox;
using Marten;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Options;
using Serilog;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddOptions<OutboxSettings>()
    .BindConfiguration("Outbox")
    .ValidateDataAnnotations()
    .ValidateOnStart();

builder.Services.AddSingleton(sp =>
    sp.GetRequiredService<IOptions<OutboxSettings>>().Value);

builder.Services.AddCarter();

builder.Services.AddMarten(options =>
{
    options.Connection(builder.Configuration.GetConnectionString("Database")!);
});

builder.Services.Configure<MessageBrokerSettings>(
    builder.Configuration.GetSection("MessageBroker"));

builder.Services.AddSingleton(sp =>
    sp.GetRequiredService<IOptions<MessageBrokerSettings>>().Value);

builder.Services.AddMassTransit(busConfigurator =>
{
    busConfigurator.SetKebabCaseEndpointNameFormatter();

    busConfigurator.AddConsumer<ProductCreatedEventConsumer>();

    busConfigurator.UsingRabbitMq((context, configurator) =>
    {
        MessageBrokerSettings settings = context.GetRequiredService<MessageBrokerSettings>();

        configurator.Host(new Uri(settings.Host), h =>
        {
            h.Username(settings.Username);
            h.Password(settings.Password);
        });

        configurator.ConfigureEndpoints(context);
    });
});

builder.Services.AddTransient<IEventBus, EventBus>();

builder.Services.AddMediatR(ApplicationAssembly.Instance);

builder.Services.AddScoped(
    typeof(IPipelineBehavior<,>),
    typeof(LoggingPipelineBehavior<,>));

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.MapCarter();

app.Run();
