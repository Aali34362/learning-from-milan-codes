namespace Modules.Training.Endpoints.Routes;

internal static class InvitationsRoutes
{
    internal const string Tag = "Invitations";

    internal const string BaseUri = "invitations";

    internal const string ResourceId = "invitationId";

    internal const string Get = $"{BaseUri}";

    internal const string GetById = $"{BaseUri}/{{{ResourceId}:guid}}";

    internal const string Cancel = $"{BaseUri}/{{{ResourceId}:guid}}";
}
