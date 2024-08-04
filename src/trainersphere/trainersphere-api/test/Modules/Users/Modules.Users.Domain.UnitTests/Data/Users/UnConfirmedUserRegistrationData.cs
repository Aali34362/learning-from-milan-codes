using Bogus;
using Modules.Users.Domain.UserRegistrations;
using Xunit;

namespace Modules.Users.Domain.UnitTests.Data.Users;

internal sealed class UnConfirmedUserRegistrationData : TheoryData<UserRegistration>
{
    public UnConfirmedUserRegistrationData()
    {
        var faker = new Faker();

        UserRegistration pendingUserRegistration = UserRegistration
            .Create(new UserRegistrationId(faker.Random.Guid()), faker.Internet.Email()).Value;

        Add(pendingUserRegistration);

        UserRegistration cancelledUserRegistration = UserRegistration
            .Create(new UserRegistrationId(faker.Random.Guid()), faker.Internet.Email()).Value;

        cancelledUserRegistration.Cancel();

        Add(cancelledUserRegistration);

        UserRegistration expiredUserRegistration = UserRegistration
            .Create(new UserRegistrationId(faker.Random.Guid()), faker.Internet.Email()).Value;

        expiredUserRegistration.Expire();

        Add(expiredUserRegistration);
    }
}
