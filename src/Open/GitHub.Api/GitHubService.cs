namespace GitHub.Api;

public sealed class GitHubService
{
    private readonly HttpClient _client;
    private static readonly Random Random = new();

    public GitHubService(HttpClient client)
    {
        _client = client;
    }

    public async Task<GitHubUser?> GetByUsernameAsync(string username, CancellationToken cancellationToken)
    {
        if (Random.NextDouble() < 0.7)
        {
            throw new ApplicationException("The GitHub API is temporarily unavailable");
        }

        var content = await _client.GetFromJsonAsync<GitHubUser>($"users/{username}", cancellationToken);

        return content;
    }
}
