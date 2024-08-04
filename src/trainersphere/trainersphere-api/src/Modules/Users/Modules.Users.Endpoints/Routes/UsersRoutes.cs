namespace Modules.Users.Endpoints.Routes;

internal static class UsersRoutes
{
    internal const string Tag = "Users";

    internal const string BaseUri = "users";

    internal const string ResourceId = "userId";

    internal const string Register = $"{BaseUri}/register";

    internal const string Update = $"{BaseUri}/{{{ResourceId}:guid}}";

    internal const string GetById = $"{BaseUri}/{{{ResourceId}:guid}}";

    internal const string GetByIdentityProviderId = $"{BaseUri}/me";
}
