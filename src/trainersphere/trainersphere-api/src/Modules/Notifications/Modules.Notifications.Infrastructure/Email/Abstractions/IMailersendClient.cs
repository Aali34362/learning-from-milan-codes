using Modules.Notifications.Infrastructure.Email.Contracts;
using Refit;

namespace Modules.Notifications.Infrastructure.Email.Abstractions;

/// <summary>
/// Represents the Mailersend client interface.
/// </summary>
internal interface IMailersendClient
{
    [Post("/email")]
    Task SendEmailAsync(EmailRequest request, CancellationToken cancellationToken = default);
}
