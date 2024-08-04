using Bogus;
using Modules.Users.Domain.UserRegistrations;
using Xunit;

namespace Modules.Users.Domain.UnitTests.Data.Users;

internal sealed class ConfirmedUserRegistrationData : TheoryData<UserRegistration>
{
    public ConfirmedUserRegistrationData()
    {
        var faker = new Faker();

        UserRegistration userRegistration = UserRegistration.Create(new UserRegistrationId(faker.Random.Guid()), faker.Internet.Email()).Value;

        userRegistration.Confirm(
            faker.Internet.Ip(),
            userRegistration.Email,
            faker.Name.FirstName(),
            faker.Name.LastName());

        Add(userRegistration);
    }
}
