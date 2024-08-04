using Domain.Primitives;

namespace Modules.Users.Domain.UserRegistrations.Events;

/// <summary>
/// Represents the domain event that is raised when a user registration is confirmed.
/// </summary>
/// <param name="Id">The identifier.</param>
/// <param name="OccurredOnUtc">The occurred on date and time.</param>
/// <param name="UserRegistrationId">The user registration identifier.</param>
public sealed record UserRegistrationConfirmedDomainEvent(
    Guid Id,
    DateTime OccurredOnUtc,
    UserRegistrationId UserRegistrationId) : DomainEvent(Id, OccurredOnUtc);
