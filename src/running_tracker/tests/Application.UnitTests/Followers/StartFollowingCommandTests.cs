using Application.Abstractions.Data;
using Application.Followers.StartFollowing;
using Domain.Followers;
using Domain.Users;
using FluentAssertions;
using NSubstitute;
using SharedKernel;

namespace Application.UnitTests.Followers;

public class StartFollowingCommandTests
{
    private static readonly User User = User.Create(
        Email.Create("test@test.com").Value,
        new Name("FullName"),
        hasPublicProfile: true);
    private static readonly StartFollowingCommand Command = new(Guid.NewGuid(), Guid.NewGuid());

    private readonly StartFollowingCommandHandler _handler;
    private readonly IUserRepository _userRepositoryMock;
    private readonly IFollowerService _followerService;
    private readonly IUnitOfWork _unitOfWorkMock;

    public StartFollowingCommandTests()
    {
        _userRepositoryMock = Substitute.For<IUserRepository>();
        _followerService = Substitute.For<IFollowerService>();
        _unitOfWorkMock = Substitute.For<IUnitOfWork>();

        _handler = new StartFollowingCommandHandler(
            _userRepositoryMock,
            _followerService,
            _unitOfWorkMock);
    }

    [Fact]
    public async Task Handle_Should_ReturnError_WhenUserNotFound()
    {
        // Arrange
        _userRepositoryMock.GetByIdAsync(Command.UserId)
            .Returns((User?)null);

        // Act
        Result result = await _handler.Handle(Command, default);

        // Assert
        result.Error.Should().Be(UserErrors.NotFound(Command.UserId));
    }

    [Fact]
    public async Task Handle_Should_ReturnError_WhenFollowedNotFound()
    {
        // Arrange
        _userRepositoryMock.GetByIdAsync(Command.UserId)
            .Returns(User);

        _userRepositoryMock.GetByIdAsync(Command.FollowedId)
            .Returns((User?)null);

        // Act
        Result result = await _handler.Handle(Command, default);

        // Assert
        result.Error.Should().Be(UserErrors.NotFound(Command.FollowedId));
    }

    [Fact]
    public async Task Handle_Should_ReturnError_WhenStartFollowingFails()
    {
        // Arrange
        _userRepositoryMock.GetByIdAsync(Command.UserId)
            .Returns(User);

        _userRepositoryMock.GetByIdAsync(Command.FollowedId)
            .Returns(User);

        _followerService.StartFollowingAsync(User, User, default)
            .Returns(FollowerErrors.SameUser);

        // Act
        Result result = await _handler.Handle(Command, default);

        // Assert
        result.Error.Should().Be(FollowerErrors.SameUser);
    }

    [Fact]
    public async Task Handle_Should_ReturnSuccess_WhenStartFollowingDoesNotFail()
    {
        // Arrange
        _userRepositoryMock.GetByIdAsync(Command.UserId)
            .Returns(User);

        _userRepositoryMock.GetByIdAsync(Command.FollowedId)
            .Returns(User);

        _followerService.StartFollowingAsync(User, User, default)
            .Returns(Result.Success());

        // Act
        Result result = await _handler.Handle(Command, default);

        // Assert
        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public async Task Handle_Should_CallUnitOfWork_WhenStartFollowingDoesNotFail()
    {
        // Arrange
        _userRepositoryMock.GetByIdAsync(Command.UserId)
            .Returns(User);

        _userRepositoryMock.GetByIdAsync(Command.FollowedId)
            .Returns(User);

        _followerService.StartFollowingAsync(User, User, default)
            .Returns(Result.Success());

        // Act
        await _handler.Handle(Command, default);

        // Assert
        await _unitOfWorkMock.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
    }
}
