using FluentValidation;

namespace GitHub.Api;

public sealed class GitHubSettingsValidator : AbstractValidator<GitHubSettings>
{
    public GitHubSettingsValidator()
    {
        RuleFor(x => x.AccessToken).NotEmpty();

        RuleFor(x => x.UserAgent).NotEmpty();

        RuleFor(x => x.BaseAddress).NotEmpty();

        RuleFor(x => x.BaseAddress)
            .Must(baseAddress => Uri.TryCreate(baseAddress, UriKind.Absolute, out _))
            .When(x => !string.IsNullOrWhiteSpace(x.BaseAddress))
            .WithMessage($"{nameof(GitHubSettings.BaseAddress)} must be a valid URL");
    }
}
