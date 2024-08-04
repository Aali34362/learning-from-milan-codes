using GitHub.Api;
using GitHub.Api.DelegatingHandlers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddOptions<GitHubSettings>()
    .BindConfiguration(GitHubSettings.ConfigurationSection)
    .ValidateDataAnnotations()
    .ValidateOnStart();

builder.Services.AddTransient<RetryHandler>();
builder.Services.AddTransient<LoggingHandler>();
builder.Services.AddTransient<GitHubAuthenticationHandler>();

builder.Services.AddHttpClient<GitHubService>(httpClient =>
{
    httpClient.BaseAddress = new Uri("https://api.github.com");
})
.AddHttpMessageHandler<RetryHandler>()
.AddHttpMessageHandler<LoggingHandler>()
.AddHttpMessageHandler<GitHubAuthenticationHandler>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapUserEndpoints();

app.Run();
