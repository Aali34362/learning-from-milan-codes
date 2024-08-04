using Gatherly.Application.Abstractions;
using Gatherly.Application.Abstractions.Messaging;
using Gatherly.Domain.DomainEvents;
using Gatherly.Domain.Entities;
using Gatherly.Domain.Repositories;
using MediatR;

namespace Gatherly.Application.Gatherings.CancelGathering;

internal sealed class OrderCancelledDomainEventHandler
    : IDomainEventHandler<OrderCancelledDomainEvent>
{
    private readonly IGatheringRepository _gatheringRepository;
    private readonly IEmailService _emailService;

    public OrderCancelledDomainEventHandler(IGatheringRepository gatheringRepository, IEmailService emailService)
    {
        _gatheringRepository = gatheringRepository;
        _emailService = emailService;
    }

    public async Task Handle(OrderCancelledDomainEvent notification, CancellationToken cancellationToken)
    {
        Gathering? gathering = await _gatheringRepository.GetByIdAsync(
            notification.GatheringId,
            cancellationToken);

        if (gathering is null)
        {
            return;
        }

        foreach (Attendee attendee in gathering.Attendees)
        {
            await _emailService.SendGatheringCancelledEmailAsync(attendee, cancellationToken);
        }
    }
}
