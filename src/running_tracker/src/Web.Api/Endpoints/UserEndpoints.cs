using Application.Followers.StartFollowing;
using Application.Users;
using Application.Users.Create;
using Application.Users.GetById;
using MediatR;
using SharedKernel;
using Web.Api.Extensions;

namespace Web.Api.Endpoints;

public static class UserEndpoints
{
    public static void MapUserEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapPost("api/users", async (CreateUserCommand command, ISender sender) =>
        {
            Result<Guid> result = await sender.Send(command);

            return result.IsSuccess ? Results.Ok(result.Value) : result.ToProblemDetails();
        });

        app.MapPost("api/users/{userId}/follow/{followedId}",
            async (Guid userId, Guid followedId, ISender sender) =>
            {
                Result result = await sender.Send(new StartFollowingCommand(userId, followedId));

                return result.IsSuccess ? Results.NoContent() : result.ToProblemDetails();
            });

        app.MapGet("api/users/{userId}", async (Guid userId, ISender sender) =>
        {
            var query = new GetUserByIdQuery(userId);

            Result<UserResponse> result = await sender.Send(query);

            return result.IsSuccess ? Results.Ok(result.Value) : result.ToNotFoundProblemDetails();
        });
    }
}
