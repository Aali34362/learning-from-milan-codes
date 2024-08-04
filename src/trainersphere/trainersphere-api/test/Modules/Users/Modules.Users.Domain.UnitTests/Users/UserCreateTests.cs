using System.Linq;
using FluentAssertions;
using FluentAssertions.Primitives;
using Modules.Users.Domain.Roles;
using Modules.Users.Domain.UnitTests.Data.Users;
using Modules.Users.Domain.Users;
using Modules.Users.Domain.Users.Events;
using Shared.Results;
using Xunit;

namespace Modules.Users.Domain.UnitTests.Users;

public sealed class UserCreateTests
{
    [Theory]
    [ClassData(typeof(CreateUserRequestData))]
    public void Create_ShouldSucceed_WhenDataIsValid(CreateUserRequest data)
    {
        // Arrange
        // Act
        Result<User> user = User.Create(data.Id, data.IdentityProviderId, data.Email, data.FirstName, data.LastName);

        // Assert
        user.IsSuccess.Should().BeTrue();
    }

    [Theory]
    [ClassData(typeof(CreateUserRequestData))]
    public void Create_ShouldCreateNewUser_WhenItSucceeds(CreateUserRequest data)
    {
        // Arrange
        // Act
        Result<User> user = User.Create(data.Id, data.IdentityProviderId, data.Email, data.FirstName, data.LastName);

        // Assert
        user.Value.Should().BeEquivalentTo(data);
    }

    [Theory]
    [ClassData(typeof(CreateUserRequestData))]
    public void Create_ShouldRaiseUserRegisteredDomainEvent_WhenItSucceeds(CreateUserRequest data)
    {
        // Arrange
        // Act
        Result<User> user = User.Create(data.Id, data.IdentityProviderId, data.Email, data.FirstName, data.LastName);

        // Assert
        AndWhichConstraint<ObjectAssertions, UserRegisteredDomainEvent> domainEvent = user.Value
            .GetDomainEvents()
            .Single(domainEvent => domainEvent is UserRegisteredDomainEvent)
            .Should()
            .BeOfType<UserRegisteredDomainEvent>();

        domainEvent.Which.UserId.Should().Be(data.Id);
        domainEvent.Which.UserRegistrationId.Should().BeNull();
        domainEvent.Which.Email.Should().Be(data.Email);
        domainEvent.Which.FirstName.Should().Be(data.FirstName);
        domainEvent.Which.LastName.Should().Be(data.LastName);
        domainEvent.Which.Roles.Should().Contain(Role.Registered.Name);
        domainEvent.Which.Roles.Should().Contain(Role.Trainer.Name);
    }

    [Theory]
    [ClassData(typeof(CreateUserRequestData))]
    public void Create_ShouldAddRegisteredRole_WhenItSucceeds(CreateUserRequest data)
    {
        // Arrange
        // Act
        Result<User> user = User.Create(data.Id, data.IdentityProviderId, data.Email, data.FirstName, data.LastName);

        // Assert
        user.Value.Roles.Should().ContainSingle(role => role == Role.Registered);
    }

    [Theory]
    [ClassData(typeof(CreateUserRequestData))]
    public void Create_ShouldAddTrainerRole_WhenItSucceeds(CreateUserRequest data)
    {
        // Arrange
        // Act
        Result<User> user = User.Create(data.Id, data.IdentityProviderId, data.Email, data.FirstName, data.LastName);

        // Assert
        user.Value.Roles.Should().ContainSingle(role => role == Role.Trainer);
    }
}
