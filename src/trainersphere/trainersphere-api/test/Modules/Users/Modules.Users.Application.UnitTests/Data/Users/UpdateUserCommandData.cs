using Bogus;
using Modules.Users.Application.Users.UpdateUser;
using Xunit;

namespace Modules.Users.Application.UnitTests.Data.Users;

internal sealed class UpdateUserCommandData : TheoryData<UpdateUserCommand>
{
    public UpdateUserCommandData()
    {
        var faker = new Faker();

        Add(new UpdateUserCommand(
            faker.Random.Guid(),
            faker.Name.FirstName(),
            faker.Name.LastName()));
    }
}
