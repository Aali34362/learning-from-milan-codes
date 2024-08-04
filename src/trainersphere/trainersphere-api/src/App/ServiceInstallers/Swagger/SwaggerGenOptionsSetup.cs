using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace App.ServiceInstallers.Swagger;

/// <summary>
/// Represents the <see cref="SwaggerGenOptions"/> setup.
/// </summary>
internal sealed class SwaggerGenOptionsSetup : IConfigureOptions<SwaggerGenOptions>
{
    /// <inheritdoc />
    public void Configure(SwaggerGenOptions options)
    {
        options.UseApiEndpoints();

        options.EnableAnnotations();

        options.SwaggerDoc("v1", new OpenApiInfo
        {
            Version = "0.0.1",
            Title = "TrainerSphere API",
            Description = "This swagger document describes the available API endpoints."
        });

        options.AddSecurityDefinition(
            JwtBearerDefaults.AuthenticationScheme,
            new OpenApiSecurityScheme
            {
                Name = HeaderNames.Authorization,
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = JwtBearerDefaults.AuthenticationScheme,
                Description = "JWT Authentication using the Bearer scheme."
            });

        options.AddSecurityRequirement(
            new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });

        options.CustomSchemaIds(type => type.FullName);

        options.OperationFilter<AuthorizeOperationFilter>();
    }
}
