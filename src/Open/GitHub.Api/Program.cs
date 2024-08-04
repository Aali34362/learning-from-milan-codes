using GitHub.Api;
using Microsoft.Extensions.Options;
using Polly;
using Polly.Fallback;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddOptions<GitHubSettings>()
    .BindConfiguration(GitHubSettings.ConfigurationSection)
    .ValidateDataAnnotations()
    .ValidateOnStart();

builder.Services.AddHttpClient<GitHubService>((sp, httpClient) =>
{
    var gitHubSettings = sp.GetRequiredService<IOptions<GitHubSettings>>().Value;

    httpClient.DefaultRequestHeaders.Add("Authorization", gitHubSettings.AccessToken);
    httpClient.DefaultRequestHeaders.Add("User-Agent", gitHubSettings.UserAgent);

    httpClient.BaseAddress = new Uri(gitHubSettings.BaseAddress);
});

builder.Services.AddResiliencePipeline<string, GitHubUser?>("gh-users-fallback",
    pipelineBuilder =>
    {
        pipelineBuilder.AddFallback(new FallbackStrategyOptions<GitHubUser?>
        {
            FallbackAction = _ => Outcome.FromResultAsValueTask<GitHubUser?>(GitHubUser.Blank)
        });
    });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapUserEndpoints();

app.Run();
