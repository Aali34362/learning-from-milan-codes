using Ardalis.ApiEndpoints;
using Endpoints.Authorization;
using Endpoints.Extensions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Modules.Training.Application.Invitations.CancelInvitation;
using Modules.Training.Endpoints.Routes;
using Shared.Results;
using Swashbuckle.AspNetCore.Annotations;

namespace Modules.Training.Endpoints.Invitations;

public sealed class CancelInvitationEndpoint : EndpointBaseAsync
    .WithRequest<Guid>
    .WithActionResult
{
    private readonly ISender _sender;

    public CancelInvitationEndpoint(ISender sender) => _sender = sender;

    [HasPermission(TrainingPermissions.CancelInvitation)]
    [HttpDelete(InvitationsRoutes.Cancel)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [SwaggerOperation(
        Summary = "Cancels the invitation.",
        Description = "Cancels the invitation with the specified identifier.",
        Tags = new[] { InvitationsRoutes.Tag })]
    public override async Task<ActionResult> HandleAsync(
        [FromRoute(Name = InvitationsRoutes.ResourceId)] Guid request,
        CancellationToken cancellationToken = default) =>
        await Result.Success(new CancelInvitationCommand(request))
            .Bind(command => _sender.Send(command, cancellationToken))
            .Match(NoContent, this.HandleFailure);
}
