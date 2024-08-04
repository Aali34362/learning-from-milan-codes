﻿using System.Threading;
using System.Threading.Tasks;
using EventReminder.Application.Core.Abstractions.Common;
using EventReminder.Domain.Core.Events;
using EventReminder.Domain.DomainEvents;
using EventReminder.Domain.Repositories;

namespace EventReminder.Application.PersonalEvents.Events.PersonalEventDateAndTimeChanged
{
    /// <summary>
    /// Represents the <see cref="PersonalEventDateAndTimeChangedDomainEvent"/> class.
    /// </summary>
    internal sealed class RemoveNotificationsOnPersonalEventDateAndTimeChangedDomainEventHandler
        : IDomainEventHandler<PersonalEventDateAndTimeChangedDomainEvent>
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IDateTime _dateTime;

        /// <summary>
        /// Initializes a new instance of the <see cref="RemoveNotificationsOnPersonalEventDateAndTimeChangedDomainEventHandler"/> class.
        /// </summary>
        /// <param name="notificationRepository">The notification repository.</param>
        /// <param name="dateTime">The date and time.</param>
        public RemoveNotificationsOnPersonalEventDateAndTimeChangedDomainEventHandler(
            INotificationRepository notificationRepository,
            IDateTime dateTime)
        {
            _notificationRepository = notificationRepository;
            _dateTime = dateTime;
        }

        /// <inheritdoc />
        public async Task Handle(PersonalEventDateAndTimeChangedDomainEvent notification, CancellationToken cancellationToken) =>
            await _notificationRepository.RemoveNotificationsForEventAsync(notification.PersonalEvent, _dateTime.UtcNow);
    }
}
