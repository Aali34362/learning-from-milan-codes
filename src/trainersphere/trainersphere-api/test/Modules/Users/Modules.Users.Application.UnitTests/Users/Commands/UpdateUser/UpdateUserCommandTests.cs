using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using Modules.Users.Application.UnitTests.Data.Users;
using Modules.Users.Application.Users.UpdateUser;
using Modules.Users.Domain;
using Modules.Users.Domain.Users;
using Moq;
using Shared.Errors;
using Shared.Results;
using Xunit;

namespace Modules.Users.Application.UnitTests.Users.Commands.UpdateUser;

public class UpdateUserCommandTests
{
    [Theory]
    [ClassData(typeof(UpdateUserCommandData))]
    public async Task Handle_ShouldFail_WhenUserIsNotFound(UpdateUserCommand command)
    {
        // Arrange
        var userRepositoryMock = new Mock<IUserRepository>();
        var userId = new UserId(command.UserId);
        userRepositoryMock
            .Setup(x => x.GetByIdAsync(It.Is<UserId>(id => id == userId), It.IsAny<CancellationToken>()))
            .ReturnsAsync(Result.Failure<User>(Error.NullValue));

        var handler = new UpdateUserCommandHandler(
            userRepositoryMock.Object,
            Mock.Of<IUnitOfWork>());

        // Act
        Result result = await handler.Handle(command, default);

        // Assert
        result.Error.Should().Be(UserErrors.NotFound(userId));
    }

    [Theory]
    [ClassData(typeof(UpdateUserCommandData))]
    public async Task Handle_ShouldChangeUserInformation_WhenUserIsFound(UpdateUserCommand command)
    {
        // Arrange
        var userRepositoryMock = new Mock<IUserRepository>();
        var userId = new UserId(command.UserId);
        Result<User> userResult = User.Create(userId, string.Empty, string.Empty, string.Empty, string.Empty);
        userRepositoryMock
            .Setup(x => x.GetByIdAsync(It.Is<UserId>(id => id == userId), It.IsAny<CancellationToken>()))
            .ReturnsAsync(userResult);

        var handler = new UpdateUserCommandHandler(
            userRepositoryMock.Object,
            Mock.Of<IUnitOfWork>());

        // Act
        await handler.Handle(command, default);

        // Assert
        userResult.Value.FirstName.Should().Be(command.FirstName);
        userResult.Value.LastName.Should().Be(command.LastName);
    }
}
