using SharedKernel;

namespace Domain.Users;

public sealed record UserCreatedDomainEvent(Guid UserId) : IDomainEvent;
