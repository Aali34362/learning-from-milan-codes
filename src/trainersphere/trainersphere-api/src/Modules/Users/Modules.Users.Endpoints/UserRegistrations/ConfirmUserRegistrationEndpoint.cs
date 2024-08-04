using Ardalis.ApiEndpoints;
using Endpoints.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Modules.Users.Application.UserRegistrations.ConfirmUserRegistration;
using Modules.Users.Endpoints.Contracts.UserRegistrations;
using Modules.Users.Endpoints.Routes;
using Shared.Results;
using Swashbuckle.AspNetCore.Annotations;

namespace Modules.Users.Endpoints.UserRegistrations;

public sealed class ConfirmUserRegistrationEndpoint : EndpointBaseAsync
    .WithRequest<ConfirmUserRegistrationRequest>
    .WithActionResult
{
    private readonly ISender _sender;

    public ConfirmUserRegistrationEndpoint(ISender sender) => _sender = sender;

    [Authorize]
    [HttpPut(UserRegistrationsRoutes.Confirm)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [SwaggerOperation(
        Summary = "Confirms the user registration.",
        Description = "Confirms the user registration based on the specified request.",
        Tags = new[] { UserRegistrationsRoutes.Tag })]
    public override async Task<ActionResult> HandleAsync(
        [FromRoute] ConfirmUserRegistrationRequest request,
        CancellationToken cancellationToken = default) =>
        await Result.Create(request)
            .Map(confirmUserRegistrationRequest => new ConfirmUserRegistrationCommand(
                confirmUserRegistrationRequest.UserRegistrationId,
                User.GetIdentityProviderId(),
                confirmUserRegistrationRequest.Content.Email,
                confirmUserRegistrationRequest.Content.FirstName,
                confirmUserRegistrationRequest.Content.LastName))
            .Bind(command => _sender.Send(command, cancellationToken))
            .Match(NoContent, this.HandleFailure);
}
