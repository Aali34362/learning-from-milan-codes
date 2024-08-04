using SharedKernel;

namespace Web.Api.Extensions;

public static class ResultExtensions
{
    public static IResult ToProblemDetails(this Result result)
    {
        if (result.IsSuccess)
        {
            throw new InvalidOperationException();
        }

        return Results.Problem(
            statusCode: StatusCodes.Status400BadRequest,
            title: "Bad Request",
            type: "https://tools.ietf.org/html/rfc7231#section-6.5.1",
            extensions: new Dictionary<string, object?>
            {
                { "errors", new[] { result.Error } }
            });
    }

    public static IResult ToNotFoundProblemDetails(this Result result)
    {
        if (result.IsSuccess)
        {
            throw new InvalidOperationException();
        }

        return Results.Problem(
            statusCode: StatusCodes.Status404NotFound,
            title: "Not Found",
            type: "https://tools.ietf.org/html/rfc7231#section-6.5.4",
            extensions: new Dictionary<string, object?>
            {
                { "errors", new[] { result.Error } }
            });
    }
}
