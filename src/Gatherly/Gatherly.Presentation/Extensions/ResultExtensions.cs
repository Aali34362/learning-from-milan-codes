using Gatherly.Domain.Shared;
using Microsoft.AspNetCore.Mvc;

namespace Gatherly.Presentation.Extensions;

internal static class ResultExtensions
{
    internal static async Task<IActionResult> Match(
        this Task<Result> resultTask,
        Func<IActionResult> onSuccess,
        Func<Result, IActionResult> onFailure)
    {
        Result result = await resultTask;

        return result.IsSuccess ? onSuccess() : onFailure(result);
    }

    internal static async Task<IActionResult> Match<TIn>(
        this Task<Result<TIn>> resultTask,
        Func<TIn, IActionResult> onSuccess,
        Func<Result, IActionResult> onFailure)
    {
        Result<TIn> result = await resultTask;

        return result.IsSuccess ? onSuccess(result.Value) : onFailure(result);
    }
}
