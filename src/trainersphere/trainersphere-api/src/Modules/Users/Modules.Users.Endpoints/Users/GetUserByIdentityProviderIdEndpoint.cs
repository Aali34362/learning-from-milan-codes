using Ardalis.ApiEndpoints;
using Endpoints.Authorization;
using Endpoints.Extensions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Modules.Users.Application.Users.GetUserByIdentityProviderId;
using Modules.Users.Endpoints.Routes;
using Shared.Results;
using Swashbuckle.AspNetCore.Annotations;

namespace Modules.Users.Endpoints.Users;

public sealed class GetUserByIdentityProviderIdEndpoint : EndpointBaseAsync
    .WithoutRequest
    .WithActionResult<UserResponse>
{
    private readonly ISender _sender;

    public GetUserByIdentityProviderIdEndpoint(ISender sender) => _sender = sender;

    [HasPermission(UsersPermissions.ReadUser)]
    [HttpGet(UsersRoutes.GetByIdentityProviderId)]
    [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [SwaggerOperation(
        Summary = "Gets the user user by identity provider id.",
        Description = "Gets the currently authenticated user by the identity provider identifier.",
        Tags = new[] { UsersRoutes.Tag })]
    public override async Task<ActionResult<UserResponse>> HandleAsync(CancellationToken cancellationToken = default) =>
        await Result.Create(new GetUserByIdentityProviderIdQuery(User.GetIdentityProviderId()))
            .Bind(query => _sender.Send(query, cancellationToken))
            .Match(Ok, this.HandleFailure);
}
