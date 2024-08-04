using FluentValidation;
using GitHub.Api;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddOptions<GitHubSettings>()
    .BindConfiguration(GitHubSettings.ConfigurationSection)
    .ValidateFluentValidation()
    .ValidateOnStart();

builder.Services.AddOptionsWithFluentValidation<GitHubSettings>(GitHubSettings.ConfigurationSection)

builder.Services.AddHttpClient<GitHubService>((sp, httpClient) =>
{
    var gitHubSettings = sp.GetRequiredService<IOptions<GitHubSettings>>().Value;

    httpClient.DefaultRequestHeaders.Add("Authorization", gitHubSettings.AccessToken);
    httpClient.DefaultRequestHeaders.Add("User-Agent", gitHubSettings.UserAgent);

    httpClient.BaseAddress = new Uri(gitHubSettings.BaseAddress);
});

builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapUserEndpoints();

try
{
    app.Run();
}
catch (Exception e)
{
    Console.WriteLine(e);
    throw;
}
