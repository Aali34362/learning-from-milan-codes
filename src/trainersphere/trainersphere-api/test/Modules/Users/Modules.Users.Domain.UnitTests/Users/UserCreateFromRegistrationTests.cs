using System.Linq;
using FluentAssertions;
using FluentAssertions.Primitives;
using Modules.Users.Domain.Roles;
using Modules.Users.Domain.UnitTests.Data.Users;
using Modules.Users.Domain.UserRegistrations;
using Modules.Users.Domain.Users;
using Modules.Users.Domain.Users.Events;
using Shared.Results;
using Xunit;

namespace Modules.Users.Domain.UnitTests.Users;

public sealed class UserCreateFromRegistrationTests
{
    [Theory]
    [ClassData(typeof(UnConfirmedUserRegistrationData))]
    public void CreateFromRegistration_ShouldFail_WhenUserRegistrationIsNotConfirmed(UserRegistration userRegistration)
    {
        // Arrange
        // Act
        Result<User> result = User.CreateFromRegistration(userRegistration);

        // Assert
        result.Error.Should().Be(UserErrors.RegistrationIsNotConfirmed);
    }

    [Theory]
    [ClassData(typeof(IncompleteUserRegistrationData))]
    public void CreateFromRegistration_ShouldFail_WhenUserRegistrationIsNotComplete(UserRegistration userRegistration)
    {
        // Arrange
        // Act
        Result<User> result = User.CreateFromRegistration(userRegistration);

        // Assert
        result.Error.Should().Be(UserErrors.RegistrationIsIncomplete);
    }

    [Theory]
    [ClassData(typeof(ConfirmedUserRegistrationData))]
    public void CreateFromRegistration_ShouldSucceed_WhenDataIsValid(UserRegistration userRegistration)
    {
        // Arrange
        // Act
        Result<User> result = User.CreateFromRegistration(userRegistration);

        // Assert
        result.IsSuccess.Should().BeTrue();
    }

    [Theory]
    [ClassData(typeof(ConfirmedUserRegistrationData))]
    public void CreateFromRegistration_ShouldCreateNewUser_WhenItSucceeds(UserRegistration userRegistration)
    {
        // Arrange
        // Act
        Result<User> result = User.CreateFromRegistration(userRegistration);

        // Assert
        result.Value.Should().NotBeNull();
    }

    [Theory]
    [ClassData(typeof(ConfirmedUserRegistrationData))]
    public void CreateFromRegistration_ShouldRaiseUserRegisteredDomainEvent_WhenItSucceeds(UserRegistration userRegistration)
    {
        // Arrange
        // Act
        Result<User> result = User.CreateFromRegistration(userRegistration);

        // Assert
        AndWhichConstraint<ObjectAssertions, UserRegisteredDomainEvent> domainEvent = result.Value
            .GetDomainEvents()
            .Single(domainEvent => domainEvent is UserRegisteredDomainEvent)
            .Should()
            .BeOfType<UserRegisteredDomainEvent>();

        domainEvent.Which.UserId.Should().Be(result.Value.Id);
        domainEvent.Which.UserRegistrationId.Should().Be(userRegistration.Id);
        domainEvent.Which.Email.Should().Be(result.Value.Email);
        domainEvent.Which.FirstName.Should().Be(result.Value.FirstName);
        domainEvent.Which.LastName.Should().Be(result.Value.LastName);
        domainEvent.Which.Roles.Should().Contain(Role.Registered.Name);
        domainEvent.Which.Roles.Should().Contain(Role.Client.Name);
    }

    [Theory]
    [ClassData(typeof(ConfirmedUserRegistrationData))]
    public void CreateFromRegistration_ShouldAddRegisteredRole_WhenItSucceeds(UserRegistration userRegistration)
    {
        // Arrange
        // Act
        Result<User> result = User.CreateFromRegistration(userRegistration);

        // Assert
        result.Value.Roles.Should().ContainSingle(role => role == Role.Registered);
    }

    [Theory]
    [ClassData(typeof(ConfirmedUserRegistrationData))]
    public void CreateFromRegistration_ShouldAddClientRole_WhenItSucceeds(UserRegistration userRegistration)
    {
        // Arrange
        // Act
        Result<User> result = User.CreateFromRegistration(userRegistration);

        // Assert
        result.Value.Roles.Should().ContainSingle(role => role == Role.Client);
    }
}
