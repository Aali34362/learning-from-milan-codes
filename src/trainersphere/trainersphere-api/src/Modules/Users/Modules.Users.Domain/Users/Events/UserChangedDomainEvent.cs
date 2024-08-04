using Domain.Primitives;

namespace Modules.Users.Domain.Users.Events;

/// <summary>
/// Represents the domain event that is raised when a user's basic information has changed.
/// </summary>
/// <param name="Id">The identifier.</param>
/// <param name="OccurredOnUtc">The occurred on date and time.</param>
/// <param name="UserId">The user identifier.</param>
/// <param name="FirstName">The first name.</param>
/// <param name="LastName">The last name.</param>
/// <param name="Roles">The roles.</param>
public sealed record UserChangedDomainEvent(
    Guid Id,
    DateTime OccurredOnUtc,
    UserId UserId,
    string FirstName,
    string LastName,
    HashSet<string> Roles) : DomainEvent(Id, OccurredOnUtc);
