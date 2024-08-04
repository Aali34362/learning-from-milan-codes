using Application;
using Application.Abstractions.Links;
using Carter;
using Infrastructure;
using Persistence;
using Web.API.Controllers;
using Web.API.Extensions;
using Web.API.Middleware;
using Web.API.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.Services.AddInfrastructure();
builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddApplication();

builder.Services.AddScoped<ILinkService, LinkService>();
builder.Services.AddHttpContextAccessor();

builder.Services.AddCarter();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.ApplyMigrations();
}

app.MapControllers();

app.MapCarter();

app.UseHttpsRedirection();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.Run();

public partial class Program { }