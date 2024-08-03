using Application;
using Carter;
using Infrastructure.Outbox;
using Marten;
using MediatR;
using Microsoft.Extensions.Options;

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

builder.Services.AddMediatR(ApplicationAssembly.Instance);

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapCarter();

app.MapGet("settings/outbox", (OutboxSettings settings) =>
{
    return Results.Ok(settings);
});

app.Run();
