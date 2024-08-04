using Ardalis.ApiEndpoints;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Errors;
using Shared.Results;

namespace Endpoints.Extensions;

/// <summary>
/// Contains extension methods for the <see cref="EndpointBase"/> class.
/// </summary>
public static class EndpointBaseExtensions
{
    /// <summary>
    /// Handles the failure result and returns the appropriate response.
    /// </summary>
    /// <param name="endpoint">The endpoint.</param>
    /// <param name="result">The failure result.</param>
    /// <returns>The appropriate response based on the result type.</returns>
    /// <exception cref="InvalidOperationException"> when this method is invoked with a success result.</exception>
    public static ActionResult HandleFailure(this EndpointBase endpoint, Result result) =>
        result switch
        {
            { IsSuccess: true } => throw new InvalidOperationException("This method can't be invoked for a success result."),
            IValidationResult validationResult =>
                endpoint.BadRequest(
                    CreateProblemDetails(
                        "Validation Error", StatusCodes.Status400BadRequest, IValidationResult.ValidationError, validationResult.Errors)),
            var notFound when notFound.Error is NotFoundError =>
                endpoint.NotFound(CreateProblemDetails("Not Found", StatusCodes.Status404NotFound, notFound.Error)),
            var conflict when conflict.Error is ConflictError =>
                endpoint.Conflict(CreateProblemDetails("Conflict", StatusCodes.Status409Conflict, conflict.Error)),
            var badRequest =>
                endpoint.BadRequest(CreateProblemDetails("Bad Request", StatusCodes.Status400BadRequest, badRequest.Error))
        };

    private static ProblemDetails CreateProblemDetails(string title, int status, Error error, Error[]? errors = null) =>
        new()
        {
            Title = title,
            Type = error.Code,
            Detail = error.Message,
            Status = status,
            Extensions = { { nameof(errors), errors } }
        };
}
