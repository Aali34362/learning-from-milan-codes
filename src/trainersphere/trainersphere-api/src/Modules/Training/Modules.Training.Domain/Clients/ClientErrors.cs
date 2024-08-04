using Shared.Errors;

namespace Modules.Training.Domain.Clients;

/// <summary>
/// Contains the client errors.
/// </summary>
public static class ClientErrors
{
    /// <summary>
    /// Gets the not found error.
    /// </summary>
    public static Func<ClientId, Error> NotFound => trainerId => new NotFoundError(
        "Client.NotFound",
        $"The client with the identifier '{trainerId.Value}' was not found.");
}
