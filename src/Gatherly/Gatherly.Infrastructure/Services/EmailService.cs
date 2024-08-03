using Gatherly.Application.Abstractions;
using Gatherly.Domain.Entities;

namespace Gatherly.Infrastructure.Services;

internal sealed class EmailService : IEmailService
{
    public Task SendInvitationSentEmailAsync(Member member, Gathering gathering, CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }

    public Task SendInvitationAcceptedEmailAsync(Gathering gathering, CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }
}