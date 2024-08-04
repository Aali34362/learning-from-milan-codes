using Ardalis.ApiEndpoints;
using Endpoints.Authorization;
using Endpoints.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Modules.Users.Application.Users.GetUserById;
using Modules.Users.Endpoints.Routes;
using Shared.Results;
using Swashbuckle.AspNetCore.Annotations;

namespace Modules.Users.Endpoints.Users;

public sealed class GetUserByIdEndpoint : EndpointBaseAsync
    .WithRequest<Guid>
    .WithActionResult<UserResponse>
{
    private readonly ISender _sender;

    public GetUserByIdEndpoint(ISender sender) => _sender = sender;

    [Authorize(Policies.SameUser)]
    [HasPermission(UsersPermissions.ReadUser)]
    [HttpGet(UsersRoutes.GetById, Name = nameof(GetUserByIdEndpoint))]
    [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [SwaggerOperation(
        Summary = "Gets the user by id.",
        Description = "Gets the user with the specified identifier.",
        Tags = new[] { UsersRoutes.Tag })]
    public override async Task<ActionResult<UserResponse>> HandleAsync(
        [FromRoute(Name = UsersRoutes.ResourceId)] Guid request,
        CancellationToken cancellationToken = default) =>
        await Result.Create(new GetUserByIdQuery(request))
            .Bind(query => _sender.Send(query, cancellationToken))
            .Match(Ok, this.HandleFailure);
}
