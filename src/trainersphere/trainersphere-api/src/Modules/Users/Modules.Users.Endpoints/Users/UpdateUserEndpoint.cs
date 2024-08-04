using Ardalis.ApiEndpoints;
using Endpoints.Authorization;
using Endpoints.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Modules.Users.Application.Users.UpdateUser;
using Modules.Users.Endpoints.Contracts.Users;
using Modules.Users.Endpoints.Routes;
using Shared.Results;
using Swashbuckle.AspNetCore.Annotations;

namespace Modules.Users.Endpoints.Users;

public sealed class UpdateUserEndpoint : EndpointBaseAsync
    .WithRequest<UpdateUserRequest>
    .WithActionResult
{
    private readonly ISender _sender;

    public UpdateUserEndpoint(ISender sender) => _sender = sender;

    [Authorize(Policies.SameUser)]
    [HasPermission(UsersPermissions.ModifyUser)]
    [HttpPut(UsersRoutes.Update)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [SwaggerOperation(
        Summary = "Updates the user's basic information.",
        Description = "Updates the user's basic information based on the specified request.",
        Tags = new[] { UsersRoutes.Tag })]
    public override async Task<ActionResult> HandleAsync([FromRoute]UpdateUserRequest request, CancellationToken cancellationToken = default) =>
        await Result.Create(request)
            .Map(updateUserRequest => new UpdateUserCommand(
                updateUserRequest.UserId,
                updateUserRequest.Content.FirstName,
                updateUserRequest.Content.LastName))
            .Bind(command => _sender.Send(command, cancellationToken))
            .Match(NoContent, this.HandleFailure);
}
