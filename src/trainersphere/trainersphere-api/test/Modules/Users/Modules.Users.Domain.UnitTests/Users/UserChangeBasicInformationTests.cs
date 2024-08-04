using System.Linq;
using FluentAssertions;
using FluentAssertions.Primitives;
using Modules.Users.Domain.UnitTests.Data.Users;
using Modules.Users.Domain.Users;
using Modules.Users.Domain.Users.Events;
using Xunit;

namespace Modules.Users.Domain.UnitTests.Users;

public sealed class UserChangeBasicInformationTests
{
    [Theory]
    [ClassData(typeof(CreateUserRequestData))]
    public void Change_ShouldChangeBasicInformation_WhenDataBasicInformationIsDifferent(CreateUserRequest data)
    {
        // Arrange
        User user = User.Create(data.Id, data.IdentityProviderId, data.Email, string.Empty, string.Empty).Value;

        // Act
        user.Change(data.FirstName, data.LastName);

        // Assert
        user.FirstName.Should().Be(data.FirstName);
        user.LastName.Should().Be(data.LastName);
    }

    [Theory]
    [ClassData(typeof(CreateUserRequestData))]
    public void Change_ShouldRaiseUserChangedDomainEvent_WhenBasicInformationHasChanged(CreateUserRequest data)
    {
        // Arrange
        User user = User.Create(data.Id, data.IdentityProviderId, data.Email, string.Empty, string.Empty).Value;
        user.ClearDomainEvents();

        // Act
        user.Change(data.FirstName, data.LastName);

        // Assert
        AndWhichConstraint<ObjectAssertions, UserChangedDomainEvent> domainEvent = user
            .GetDomainEvents()
            .Single()
            .Should()
            .BeOfType<UserChangedDomainEvent>();

        domainEvent.Which.UserId.Should().Be(data.Id);
        domainEvent.Which.FirstName.Should().Be(data.FirstName);
        domainEvent.Which.LastName.Should().Be(data.LastName);
    }
}
