using SharedKernel;

namespace Domain.Users;

public static class UserErrors
{
    public static Error NotFound(Guid userId) => new(
        "Users.NotFound", $"The user with the Id = '{userId}' was not found");
}
