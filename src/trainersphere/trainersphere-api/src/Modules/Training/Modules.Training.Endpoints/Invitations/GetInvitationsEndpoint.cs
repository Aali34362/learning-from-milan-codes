using Ardalis.ApiEndpoints;
using Endpoints.Authorization;
using Endpoints.Extensions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Modules.Training.Application.Invitations.GetInvitations;
using Modules.Training.Endpoints.Routes;
using Shared.Results;
using Swashbuckle.AspNetCore.Annotations;

namespace Modules.Training.Endpoints.Invitations;

public sealed class GetInvitationsEndpoint : EndpointBaseAsync
    .WithRequest<Guid>
    .WithActionResult<List<InvitationResponse>>
{
    private readonly ISender _sender;

    public GetInvitationsEndpoint(ISender sender) => _sender = sender;

    [HasPermission(TrainingPermissions.ReadInvitations)]
    [HttpGet(InvitationsRoutes.Get)]
    [ProducesResponseType(typeof(InvitationResponse), StatusCodes.Status200OK)]
    [SwaggerOperation(
        Summary = "Gets the invitations.",
        Description = "Gets the invitation with the specified trainer identifier.",
        Tags = new[] { InvitationsRoutes.Tag })]
    public override async Task<ActionResult<List<InvitationResponse>>> HandleAsync(
        [FromQuery(Name = TrainersRoutes.ResourceId)] Guid request,
        CancellationToken cancellationToken = default) =>
        await Result.Success(new GetInvitationsQuery(request))
            .Bind(query => _sender.Send(query, cancellationToken))
            .Match(Ok, this.HandleFailure);
}
