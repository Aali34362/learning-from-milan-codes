using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Modules.Users.Application.UnitTests.Data.Users;
using Modules.Users.Application.Users.RegisterUser;
using Modules.Users.Domain;
using Modules.Users.Domain.Users;
using Moq;
using Shared.Errors;
using Shared.Results;
using Xunit;

namespace Modules.Users.Application.UnitTests.Users.Commands.RegisterUser;

public sealed class RegisterUserCommandHandlerTests
{
    [Theory]
    [ClassData(typeof(RegisterUserCommandData))]
    public async Task Handle_ShouldFail_WhenIdentityDoesNotExist(RegisterUserCommand command)
    {
        // Arrange
        var identityProviderServiceMock = new Mock<IIdentityProviderService>();
        identityProviderServiceMock
            .Setup(x => x.ExistsAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Failure(Error.ConditionNotMet));

        var handler = new RegisterUserCommandHandler(
            identityProviderServiceMock.Object,
            Mock.Of<IUserRepository>(),
            Mock.Of<IUnitOfWork>());

        // Act
        Result<Guid> result = await handler.Handle(command, default);

        // Assert
        result.Error.Should().Be(UserErrors.NotFoundByIdentity(command.IdentityProviderId));
    }

    [Theory]
    [ClassData(typeof(RegisterUserCommandData))]
    public async Task Handle_ShouldFail_WhenEmailIsNotUnique(RegisterUserCommand command)
    {
        // Arrange
        var identityProviderServiceMock = new Mock<IIdentityProviderService>();
        identityProviderServiceMock
            .Setup(x => x.ExistsAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Success);

        var userRepositoryMock = new Mock<IUserRepository>();
        userRepositoryMock
            .Setup(x => x.IsEmailUniqueAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Failure(Error.ConditionNotMet));

        var handler = new RegisterUserCommandHandler(
            identityProviderServiceMock.Object,
            userRepositoryMock.Object,
            Mock.Of<IUnitOfWork>());

        // Act
        Result<Guid> result = await handler.Handle(command, default);

        // Assert
        result.Error.Should().Be(UserErrors.EmailIsNotUnique);
    }

    [Theory]
    [ClassData(typeof(RegisterUserCommandData))]
    public async Task Handle_ShouldSucceed_WhenIdentityExistsAndEmailIsUnique(RegisterUserCommand command)
    {
        // Arrange
        var identityProviderServiceMock = new Mock<IIdentityProviderService>();
        identityProviderServiceMock
            .Setup(x => x.ExistsAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Success);

        var userRepositoryMock = new Mock<IUserRepository>();
        userRepositoryMock
            .Setup(x => x.IsEmailUniqueAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Success);

        var handler = new RegisterUserCommandHandler(
            identityProviderServiceMock.Object,
            userRepositoryMock.Object,
            Mock.Of<IUnitOfWork>());

        // Act
        Result<Guid> result = await handler.Handle(command, default);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeEmpty();
    }

    [Theory]
    [ClassData(typeof(RegisterUserCommandData))]
    public async Task Handle_ShouldCreateUser_WhenIdentityExistsAndEmailIsUnique(RegisterUserCommand command)
    {
        // Arrange
        var identityProviderServiceMock = new Mock<IIdentityProviderService>();
        identityProviderServiceMock
            .Setup(x => x.ExistsAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Success);

        var userRepositoryMock = new Mock<IUserRepository>();
        userRepositoryMock
            .Setup(x => x.IsEmailUniqueAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Success);

        var handler = new RegisterUserCommandHandler(
            identityProviderServiceMock.Object,
            userRepositoryMock.Object,
            Mock.Of<IUnitOfWork>());

        // Act
        Result<Guid> result = await handler.Handle(command, default);

        // Assert
        userRepositoryMock.Verify(
            x => x.Add(
                It.Is<User>(
                    u => u.Id.Value == result.Value &&
                         u.IdentityProviderId == command.IdentityProviderId &&
                         u.Email == command.Email &&
                         u.FirstName == command.FirstName &&
                         u.LastName == command.LastName)),
            Times.Once);
    }
}
