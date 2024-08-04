using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.EventBus;
using Modules.Users.Application.UnitTests.Data.Users;
using Modules.Users.Application.Users.RegisterUser;
using Modules.Users.Domain.Users.Events;
using Modules.Users.IntegrationEvents;
using Moq;
using Xunit;

namespace Modules.Users.Application.UnitTests.Users.Commands.RegisterUser;

public sealed class UserRegisteredDomainEventHandlerTests
{
    [Theory]
    [ClassData(typeof(UserRegisteredDomainEventData))]
    public async Task Handle_ShouldPublishIntegrationEvent_WhenDomainEventIsHandled(UserRegisteredDomainEvent domainEvent)
    {
        // Arrange
        var eventBusMock = new Mock<IEventBus>();
        var handler = new UserRegisteredDomainEventHandler(eventBusMock.Object);

        // Act
        await handler.Handle(domainEvent, default);

        // Assert
        Guid? userRegistrationId = domainEvent.UserRegistrationId?.Value;
        eventBusMock.Verify(
            x => x.PublishAsync(
                It.Is<UserRegisteredIntegrationEvent>(
                    integrationEvent =>
                        integrationEvent.Id == domainEvent.Id &&
                        integrationEvent.OccurredOnUtc == domainEvent.OccurredOnUtc &&
                        integrationEvent.UserId == domainEvent.UserId.Value &&
                        integrationEvent.UserRegistrationId == userRegistrationId &&
                        integrationEvent.Email == domainEvent.Email &&
                        integrationEvent.FirstName == domainEvent.FirstName &&
                        integrationEvent.LastName == domainEvent.LastName &&
                        integrationEvent.Roles.SequenceEqual(domainEvent.Roles)),
                It.IsAny<CancellationToken>()),
            Times.Once);
    }
}
