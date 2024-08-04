using Api.Endpoints;
using Api.Extensions;
using Application.Articles.CreateArticle;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Persistence.Database;
using Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(
    typeof(CreateArticleCommand).Assembly,
    typeof(ApplicationDbContext).Assembly));

builder.Services.AddScoped<IArticleRepository, ArticleRepository>();

builder.Services.AddDbContext<ApplicationDbContext>(o =>
    o.UseSqlServer(builder.Configuration.GetConnectionString("Database")));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.ApplyMigrations();
}

app.MapArticleEndpoints();

app.UseHttpsRedirection();

app.Run();
