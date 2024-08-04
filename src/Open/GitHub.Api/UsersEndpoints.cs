using Microsoft.AspNetCore.Mvc.Formatters;
using Polly;
using Polly.Fallback;
using Polly.Registry;
using Polly.Retry;

namespace GitHub.Api;

public static class UsersEndpoints
{
    public static void MapUserEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("api/users/{username}", async (
            string username,
            GitHubService gitHubService,
            ResiliencePipelineProvider<string> pipelineProvider,
            CancellationToken cancellationToken) =>
        {
            var policy = Policy
                .Handle<ApplicationException>()
                .RetryAsync(2, (ex, retryCount) =>
                {
                    Console.WriteLine($"Current attempt: {retryCount}, {ex}");
                });

            //var pipeline = new ResiliencePipelineBuilder<GitHubUser?>()
            //    .AddRetry(new RetryStrategyOptions<GitHubUser?>
            //    {
            //        MaxRetryAttempts = 2,
            //        BackoffType = DelayBackoffType.Constant,
            //        Delay = TimeSpan.Zero,
            //        ShouldHandle = new PredicateBuilder<GitHubUser?>()
            //            .Handle<ApplicationException>(),
            //        OnRetry = retryArguments =>
            //        {
            //            Console.WriteLine(
            //                $"Current attempt: {retryArguments.AttemptNumber}, {retryArguments.Outcome.Exception}");

            //            return ValueTask.CompletedTask;
            //        }
            //    })
            //    .Build();

            //var user = await policy.ExecuteAsync(() =>
            //    gitHubService.GetByUsernameAsync(username));

            //var pipeline = new ResiliencePipelineBuilder<GitHubUser?>()
            //    .AddFallback(new FallbackStrategyOptions<GitHubUser?>
            //    {
            //        FallbackAction = _ => Outcome.FromResultAsValueTask<GitHubUser?>(GitHubUser.Blank)
            //    })
            //    .Build();

            var pipeline = pipelineProvider.GetPipeline<GitHubUser?>("gh-users-fallback");


            var user = await pipeline.ExecuteAsync(async token =>
                await gitHubService.GetByUsernameAsync(username, token),
                cancellationToken);

            return Results.Ok(user);
        });
    }
}
