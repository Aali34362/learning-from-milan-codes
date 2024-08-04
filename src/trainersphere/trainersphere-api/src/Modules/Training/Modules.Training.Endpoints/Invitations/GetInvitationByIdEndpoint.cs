using Ardalis.ApiEndpoints;
using Endpoints.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Modules.Training.Application.Invitations.GetInvitationById;
using Modules.Training.Endpoints.Routes;
using Shared.Results;
using Swashbuckle.AspNetCore.Annotations;

namespace Modules.Training.Endpoints.Invitations;

public sealed class GetInvitationByIdEndpoint : EndpointBaseAsync
    .WithRequest<Guid>
    .WithActionResult<InvitationResponse>
{
    private readonly ISender _sender;

    public GetInvitationByIdEndpoint(ISender sender) => _sender = sender;

    [AllowAnonymous]
    [HttpGet(InvitationsRoutes.GetById, Name = nameof(GetInvitationByIdEndpoint))]
    [ProducesResponseType(typeof(InvitationResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [SwaggerOperation(
        Summary = "Gets the invitation by id.",
        Description = "Gets the invitation with the specified identifier.",
        Tags = new[] { InvitationsRoutes.Tag })]
    public override async Task<ActionResult<InvitationResponse>> HandleAsync(
        [FromRoute(Name = InvitationsRoutes.ResourceId)] Guid request,
        CancellationToken cancellationToken = default) =>
        await Result.Create(new GetInvitationByIdQuery(request))
            .Bind(query => _sender.Send(query, cancellationToken))
            .Match(Ok, this.HandleFailure);
}
