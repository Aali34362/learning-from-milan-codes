using Application;
using Application.Orders.Create;
using Carter;
using Persistence;
using Rebus.Config;
using Rebus.Routing.TypeBased;
using Web.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddCarter();

builder.Services.AddRebus(rebus => rebus
    .Routing(r =>
        r.TypeBased().MapAssemblyOf<ApplicationAssemblyReference>("eshop-queue"))
    .Transport(t =>
        t.UseRabbitMq(
            builder.Configuration.GetConnectionString("MessageBroker"),
            "eshop-queue"))
    .Sagas(s =>
        s.StoreInPostgres(
            builder.Configuration.GetConnectionString("Database"),
            "sagas",
            "saga_indexes")),
    onCreated: async bus =>
    {
        await bus.Subscribe<OrderConfirmationEmailSent>();
        await bus.Subscribe<OrderPaymentRequestSent>();
    });

builder.Services.AutoRegisterHandlersFromAssemblyOf<ApplicationAssemblyReference>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.ApplyMigrations();
}

app.UseHttpsRedirection();

app.MapCarter();

app.Run();
