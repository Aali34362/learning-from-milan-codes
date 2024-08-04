using Application.EventBus;

namespace Modules.Users.IntegrationEvents;

/// <summary>
/// Represents the user changed integration event.
/// </summary>
/// <param name="Id">The identifier.</param>
/// <param name="OccurredOnUtc">The occurred on date and time.</param>
/// <param name="UserId">The user identifier.</param>
/// <param name="FirstName">The first name.</param>
/// <param name="LastName">The last name.</param>
/// <param name="Roles">The roles.</param>
public sealed record UserChangedIntegrationEvent(
    Guid Id,
    DateTime OccurredOnUtc,
    Guid UserId,
    string FirstName,
    string LastName,
    HashSet<string> Roles) : IntegrationEvent(Id, OccurredOnUtc);
