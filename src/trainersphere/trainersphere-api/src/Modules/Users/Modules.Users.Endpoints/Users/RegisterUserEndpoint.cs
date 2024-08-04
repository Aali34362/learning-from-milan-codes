using Ardalis.ApiEndpoints;
using Endpoints.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Modules.Users.Application.Users.RegisterUser;
using Modules.Users.Endpoints.Contracts.Users;
using Modules.Users.Endpoints.Routes;
using Shared.Results;
using Swashbuckle.AspNetCore.Annotations;

namespace Modules.Users.Endpoints.Users;

public sealed class RegisterUserEndpoint : EndpointBaseAsync
    .WithRequest<RegisterUserRequest>
    .WithActionResult<Guid>
{
    private readonly ISender _sender;

    public RegisterUserEndpoint(ISender sender) => _sender = sender;

    [Authorize]
    [HttpPost(UsersRoutes.Register)]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [SwaggerOperation(
        Summary = "Registers a new user.",
        Description = "Registers a new user based on the specified request.",
        Tags = new[] { UsersRoutes.Tag })]
    public override async Task<ActionResult<Guid>> HandleAsync(RegisterUserRequest request, CancellationToken cancellationToken = default) =>
        await Result.Create(request)
            .Map(registerUserRequest => new RegisterUserCommand(
                User.GetIdentityProviderId(),
                registerUserRequest.Email,
                registerUserRequest.FirstName,
                registerUserRequest.LastName))
            .Bind(command => _sender.Send(command, cancellationToken))
            .Match(userId => CreatedAtRoute(nameof(GetUserByIdEndpoint), new { userId }, userId), this.HandleFailure);
}
