using System.Globalization;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace App.ServiceInstallers.Swagger;

/// <summary>
/// Represents the authorize operation filter.
/// </summary>
internal sealed class AuthorizeOperationFilter : IOperationFilter
{
    private static readonly HttpStatusCode[] ResponseStatusCodes =
    {
        HttpStatusCode.Unauthorized,
        HttpStatusCode.Forbidden
    };

    /// <inheritdoc />
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        object[] customAttributes = context.MethodInfo.GetCustomAttributes(true);

        object[] declaringTypeCustomAttributes = context.MethodInfo.DeclaringType!.GetCustomAttributes(true);

        bool isAuthorized = declaringTypeCustomAttributes.OfType<AuthorizeAttribute>().Any() ||
                            customAttributes.OfType<AuthorizeAttribute>().Any();

        bool isAnonymousAllowed = declaringTypeCustomAttributes.OfType<AllowAnonymousAttribute>().Any() ||
                                  customAttributes.OfType<AllowAnonymousAttribute>().Any();

        if (!isAuthorized || isAnonymousAllowed)
        {
            return;
        }

        foreach (HttpStatusCode statusCode in ResponseStatusCodes)
        {
            operation.Responses.TryAdd(
                ((int)statusCode).ToString(CultureInfo.InvariantCulture),
                new OpenApiResponse
                {
                    Description = statusCode.ToString()
                });
        }
    }
}
