using Application.EventBus;

namespace Modules.Users.IntegrationEvents;

/// <summary>
/// Represents the user registered integration event.
/// </summary>
/// <param name="Id">The identifier.</param>
/// <param name="OccurredOnUtc">The occurred on date and time.</param>
/// <param name="UserId">The user identifier.</param>
/// <param name="UserRegistrationId">The user registration identifier.</param>
/// <param name="Email">The email.</param>
/// <param name="FirstName">The first name.</param>
/// <param name="LastName">The last name.</param>
/// <param name="Roles">The roles.</param>
public sealed record UserRegisteredIntegrationEvent(
    Guid Id,
    DateTime OccurredOnUtc,
    Guid UserId,
    Guid? UserRegistrationId,
    string Email,
    string FirstName,
    string LastName,
    HashSet<string> Roles) : IntegrationEvent(Id, OccurredOnUtc);
