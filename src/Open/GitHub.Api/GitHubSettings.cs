using System.ComponentModel.DataAnnotations;

namespace GitHub.Api;

public sealed class GitHubSettings
{
    public const string ConfigurationSection = "GitHub";

    [Required]
    public string AccessToken { get; init; } = string.Empty;

    [Required]
    public string UserAgent { get; init; } = string.Empty;
}
