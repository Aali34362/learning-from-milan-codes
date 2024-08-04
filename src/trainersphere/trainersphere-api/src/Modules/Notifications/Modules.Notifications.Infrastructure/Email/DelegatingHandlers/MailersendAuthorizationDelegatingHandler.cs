using System.Net.Http.Headers;
using Microsoft.Extensions.Options;
using Modules.Notifications.Infrastructure.Email.Configuration;

namespace Modules.Notifications.Infrastructure.Email.DelegatingHandlers;

/// <summary>
/// Represents the Mailersend authorization delegating handler.
/// </summary>
internal sealed class MailersendAuthorizationDelegatingHandler : DelegatingHandler
{
    private readonly MailersendOptions _options;

    /// <summary>
    /// Initializes a new instance of the <see cref="MailersendAuthorizationDelegatingHandler"/> class.
    /// </summary>
    /// <param name="options">The options.</param>
    public MailersendAuthorizationDelegatingHandler(IOptions<MailersendOptions> options) => _options = options.Value;

    /// <inheritdoc />
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _options.AccessToken);

        return await base.SendAsync(request, cancellationToken);
    }
}
