using Microsoft.Extensions.Options;

namespace GitHub.Api.DelegatingHandlers;

public class GitHubAuthenticationHandler : DelegatingHandler
{
    private readonly GitHubSettings _gitHubSettings;

    public GitHubAuthenticationHandler(IOptions<GitHubSettings> options)
    {
        _gitHubSettings = options.Value;
    }

    protected override Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        request.Headers.Add("Authorization", _gitHubSettings.AccessToken);
        request.Headers.Add("User-Agent", _gitHubSettings.UserAgent);

        return base.SendAsync(request, cancellationToken);
    }
}
