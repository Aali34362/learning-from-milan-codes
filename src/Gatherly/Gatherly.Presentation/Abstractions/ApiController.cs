using Gatherly.Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gatherly.Presentation.Abstractions;

[ApiController]
public abstract class ApiController : ControllerBase
{
    protected readonly ISender Sender;

    protected ApiController(ISender sender) => Sender = sender;

    protected IActionResult HandleFailure(Result result) =>
        result switch
        {
            { IsSuccess: true } => throw new InvalidOperationException(),
            _ =>
                BadRequest(
                    CreateProblemDetails(
                        "Bad Request",
                        "Bad Request",
                        "One or more errors occurred",
                        StatusCodes.Status400BadRequest,
                        result.Errors))
        };

    private static ProblemDetails CreateProblemDetails(
        string title,
        string type,
        string detail,
        int status,
        Error[]? errors = null) =>
        new()
        {
            Title = title,
            Type = type,
            Detail = detail,
            Status = status,
            Extensions = { { nameof(errors), errors } }
        };
}
