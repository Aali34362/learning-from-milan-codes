using Ardalis.ApiEndpoints;
using Endpoints.Authorization;
using Endpoints.Extensions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Modules.Training.Application.Trainers.InviteClient;
using Modules.Training.Endpoints.Contracts.Invitations;
using Modules.Training.Endpoints.Routes;
using Shared.Results;
using Swashbuckle.AspNetCore.Annotations;

namespace Modules.Training.Endpoints.Trainers;

public sealed class InviteClientEndpoint : EndpointBaseAsync
    .WithRequest<InviteClientRequest>
    .WithActionResult<Guid>
{
    private readonly ISender _sender;

    public InviteClientEndpoint(ISender sender) => _sender = sender;

    [HasPermission(TrainingPermissions.InviteClient)]
    [HttpPost(TrainersRoutes.InviteClient)]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [SwaggerOperation(
        Summary = "Invites the client to train with the trainer.",
        Description = "Invites the client to train with the trainer based on the specified request.",
        Tags = new[] { TrainersRoutes.Tag })]
    public override async Task<ActionResult<Guid>> HandleAsync(
        [FromRoute]InviteClientRequest request, CancellationToken cancellationToken = default) =>
        await Result.Create(request)
            .Map(inviteClientRequest => new InviteClientCommand(
                inviteClientRequest.TrainerId,
                inviteClientRequest.Content.Email))
            .Bind(command => _sender.Send(command, cancellationToken))
            .Match(invitationId => Ok(invitationId), this.HandleFailure);
}
