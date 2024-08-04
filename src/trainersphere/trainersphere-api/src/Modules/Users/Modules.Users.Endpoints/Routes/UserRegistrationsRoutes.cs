namespace Modules.Users.Endpoints.Routes;

internal static class UserRegistrationsRoutes
{
    internal const string Tag = "UserRegistrations";

    internal const string BaseUri = "user-registrations";

    internal const string ResourceId = "userRegistrationId";

    internal const string Confirm = $"{BaseUri}/{{{ResourceId}:guid}}/confirm";
}
