using Domain.Followers;
using Domain.Users;
using FluentAssertions;
using NSubstitute;
using SharedKernel;

namespace Domain.UnitTests.Followers;

public class FollowerServiceTests
{
    private readonly FollowerService _followerService;
    private readonly IFollowerRepository _followerRepositoryMock;
    private static readonly Email Email = Email.Create("test@test.com").Value;
    private static readonly Name Name = new("Full name");
    private static readonly DateTime UtcNow = DateTime.UtcNow;

    public FollowerServiceTests()
    {
        _followerRepositoryMock = Substitute.For<IFollowerRepository>();
        IDateTimeProvider dateTimeProvider = Substitute.For<IDateTimeProvider>();
        dateTimeProvider.UtcNow.Returns(UtcNow);

        _followerService = new FollowerService(
            _followerRepositoryMock,
            dateTimeProvider);
    }

    [Fact]
    public async Task StartFollowingAsync_Should_ReturnError_WhenFollowingSameUser()
    {
        // Arrange
        var user = User.Create(Email, Name, hasPublicProfile: false);

        // Act
        Result result = await _followerService.StartFollowingAsync(user, user, default);

        // Assert
        result.Error.Should().Be(FollowerErrors.SameUser);
    }

    [Fact]
    public async Task StartFollowingAsync_Should_ReturnError_WhenFollowingNonPublicProfile()
    {
        // Arrange
        var user = User.Create(Email, Name, hasPublicProfile: false);
        var followed = User.Create(Email, Name, hasPublicProfile: false);

        // Act
        Result result = await _followerService.StartFollowingAsync(user, followed, default);

        // Assert
        result.Error.Should().Be(FollowerErrors.NonPublicProfile);
    }

    [Fact]
    public async Task StartFollowingAsync_Should_ReturnError_WhenAlreadyFollowing()
    {
        // Arrange
        var user = User.Create(Email, Name, hasPublicProfile: false);
        var followed = User.Create(Email, Name, hasPublicProfile: true);

        _followerRepositoryMock
            .IsAlreadyFollowingAsync(user.Id, followed.Id, default)
            .Returns(true);

        // Act
        Result result = await _followerService.StartFollowingAsync(user, followed, default);

        // Assert
        result.Error.Should().Be(FollowerErrors.AlreadyFollowing);
    }

    [Fact]
    public async Task StartFollowingAsync_Should_ReturnSuccess_WhenFollowerCreated()
    {
        // Arrange
        var user = User.Create(Email, Name, hasPublicProfile: false);
        var followed = User.Create(Email, Name, hasPublicProfile: true);

        _followerRepositoryMock
            .IsAlreadyFollowingAsync(user.Id, followed.Id, default)
            .Returns(false);

        // Act
        Result result = await _followerService.StartFollowingAsync(user, followed, default);

        // Assert
        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public async Task StartFollowingAsync_Should_CallInsertOnRepository_WhenFollowerCreated()
    {
        // Arrange
        var user = User.Create(Email, Name, hasPublicProfile: false);
        var followed = User.Create(Email, Name, hasPublicProfile: true);

        _followerRepositoryMock
            .IsAlreadyFollowingAsync(user.Id, followed.Id, default)
            .Returns(false);

        // Act
        await _followerService.StartFollowingAsync(user, followed, default);

        // Assert
        _followerRepositoryMock.Received(1)
            .Insert(Arg.Is<Follower>(f => f.UserId == user.Id &&
                                          f.FollowedId == followed.Id &&
                                          f.CreatedOnUtc == UtcNow));
    }
}
