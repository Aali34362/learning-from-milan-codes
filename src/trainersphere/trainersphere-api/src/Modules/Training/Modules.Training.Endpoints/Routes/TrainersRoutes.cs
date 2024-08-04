namespace Modules.Training.Endpoints.Routes;

internal static class TrainersRoutes
{
    internal const string Tag = "Trainers";

    internal const string BaseUri = "trainers";

    internal const string ResourceId = "trainerId";

    internal const string InviteClient = $"{BaseUri}/{{{ResourceId}:guid}}/invite";
}
