using Microsoft.AspNetCore.Mvc;
using Modules.Training.Endpoints.Routes;

namespace Modules.Training.Endpoints.Contracts.Invitations;

/// <summary>
/// Represents the request for inviting a client.
/// </summary>
public sealed class InviteClientRequest
{
    /// <summary>
    /// Gets the trainer identifier.
    /// </summary>
    [FromRoute(Name = TrainersRoutes.ResourceId)]
    public Guid TrainerId { get; init; }

    /// <summary>
    /// Gets the content.
    /// </summary>
    [FromBody]
    public InviteClientRequestContent Content { get; init; } = new(string.Empty);
}
