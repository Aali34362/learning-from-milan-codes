using Modules.Users.Domain.Users;

namespace Modules.Users.Domain.UnitTests.Data.Users;

public sealed record CreateUserRequest(UserId Id, string IdentityProviderId, string Email, string FirstName, string LastName);
