using Gatherly.Application.Invitations.AcceptInvitation;
using Gatherly.Presentation.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Gatherly.Presentation.Controllers;

[Route("api/gatherings/{gatheringId:guid}/invitations")]
public class InvitationsController : ApiController
{
    public InvitationsController(ISender sender)
        : base(sender)
    {
    }

    [HttpPut("{id:guid}/accept")]
    public async Task<IActionResult> AcceptInvitation(
        Guid gatheringId,
        Guid id,
        CancellationToken cancellationToken)
    {
        var command = new AcceptInvitationCommand(gatheringId, id);

        await Sender.Send(command, cancellationToken);

        return NoContent();
    }
}
