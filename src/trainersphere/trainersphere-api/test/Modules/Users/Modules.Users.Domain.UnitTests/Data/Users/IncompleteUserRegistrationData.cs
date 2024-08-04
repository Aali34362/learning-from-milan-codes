using Bogus;
using Modules.Users.Domain.UserRegistrations;
using Xunit;

namespace Modules.Users.Domain.UnitTests.Data.Users;

internal sealed class IncompleteUserRegistrationData : TheoryData<UserRegistration>
{
    public IncompleteUserRegistrationData()
    {
        var faker = new Faker();

        Add(CreateIncompleteUserRegistration(string.Empty, faker.Name.FirstName(), faker.Name.LastName(), faker));

        Add(CreateIncompleteUserRegistration(faker.Internet.Ip(), string.Empty, faker.Name.LastName(), faker));

        Add(CreateIncompleteUserRegistration(faker.Internet.Ip(), faker.Name.FirstName(), string.Empty, faker));
    }

    private static UserRegistration CreateIncompleteUserRegistration(
        string identityProviderId,
        string firstName,
        string lastName,
        Faker faker)
    {
        UserRegistration userRegistration = UserRegistration.Create(new UserRegistrationId(faker.Random.Guid()), faker.Internet.Email()).Value;

        userRegistration.Confirm(
            identityProviderId,
            userRegistration.Email,
            firstName,
            lastName);

        return userRegistration;
    }
}
