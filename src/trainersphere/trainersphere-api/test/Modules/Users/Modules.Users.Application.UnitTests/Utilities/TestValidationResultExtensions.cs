using System.Collections.Generic;
using System.Linq;
using FluentValidation.TestHelper;
using Shared.Errors;

namespace Modules.Users.Application.UnitTests.Utilities;

/// <summary>
/// Contains extensions methods for the <see cref="TestValidationResult{T}"/> class.
/// </summary>
internal static class TestValidationResultExtensions
{
    /// <summary>
    /// Gets the errors of the test validation result.
    /// </summary>
    /// <typeparam name="T">The validated type.</typeparam>
    /// <param name="result">The result.</param>
    /// <returns>The list of validation errors.</returns>
    internal static List<Error> GetErrors<T>(this TestValidationResult<T> result) =>
        result.Errors
            .Select(validationFailure => new Error(validationFailure.ErrorCode, validationFailure.ErrorMessage))
            .ToList();
}
