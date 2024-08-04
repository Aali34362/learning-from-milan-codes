namespace GitHub.Api;

public static class UsersEndpoints
{
    public static void MapUserEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("api/users/{username}", async (
            string username,
            GitHubService gitHubService) =>
        {
            var content = await gitHubService.GetByUsernameAsync(username);

            return Results.Ok(content);
        });
    }
}
