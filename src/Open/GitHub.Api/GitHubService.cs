namespace GitHub.Api;

public sealed class GitHubService
{
    private readonly HttpClient _client;

    public GitHubService(HttpClient client)
    {
        _client = client;
    }

    public async Task<GitHubUser?> GetByUsernameAsync(string username)
    {
        var content = await _client.GetFromJsonAsync<GitHubUser>($"users/{username}");

        return content;
    }
}
