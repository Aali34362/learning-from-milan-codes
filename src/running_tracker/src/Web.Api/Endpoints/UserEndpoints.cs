using Application.Followers.StartFollowing;
using Application.Users.Create;
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
    }
}
