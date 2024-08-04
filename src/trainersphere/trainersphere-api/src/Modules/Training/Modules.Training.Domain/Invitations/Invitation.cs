using Domain.Primitives;
using Domain.Time;
using Modules.Training.Domain.Clients;
using Modules.Training.Domain.Invitations.Events;
using Modules.Training.Domain.Trainers;
using Shared.Results;

namespace Modules.Training.Domain.Invitations;

/// <summary>
/// Represents the invitation entity.
/// </summary>
public sealed class Invitation : Entity<InvitationId>, IAuditable
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Invitation"/> class.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <param name="trainerId">The trainer identifier.</param>
    /// <param name="email">The email.</param>
    /// <param name="sender">The sender.</param>
    /// <param name="status">The status.</param>
    private Invitation(InvitationId id, TrainerId trainerId, string email, Sender sender, InvitationStatus status)
        : this(id, trainerId, email, status) =>
        Sender = sender;

    /// <summary>
    /// Initializes a new instance of the <see cref="Invitation"/> class.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <param name="trainerId">The trainer identifier.</param>
    /// <param name="email">The email.</param>
    /// <param name="status">The status.</param>
    private Invitation(InvitationId id, TrainerId trainerId, string email, InvitationStatus status)
         : base(id)
    {
        TrainerId = trainerId;
        Email = email;
        Status = status;
        Sender = new Sender(string.Empty, string.Empty);
    }

    /// <summary>
    /// Gets the trainer identifier.
    /// </summary>
    public TrainerId TrainerId { get; private set; }

    /// <summary>
    /// Gets the email.
    /// </summary>
    public string Email { get; private set; }

    /// <summary>
    /// Gets the sender.
    /// </summary>
    public Sender Sender { get; private set; }

    /// <summary>
    /// Gets the status.
    /// </summary>
    public InvitationStatus Status { get; private set; }

    /// <inheritdoc />
    public DateTime CreatedOnUtc { get; private set; }

    /// <inheritdoc />
    public DateTime? ModifiedOnUtc { get; private set; }

    /// <summary>
    /// Accepts the invitation with the specified parameters.
    /// </summary>
    /// <param name="clientId">The client identifier.</param>
    /// <param name="email">The email.</param>
    /// <param name="firstName">The first name.</param>
    /// <param name="lastName">The last name.</param>
    /// <returns>The new client that has accepted the invitation.</returns>
    public Result<Client> Accept(ClientId clientId, string email, string firstName, string lastName)
    {
        if (Email != email)
        {
            return Result.Failure<Client>(InvitationErrors.EmailDoesNotMatch);
        }

        return Status switch
        {
            var status when status == InvitationStatus.Cancelled => Result.Failure<Client>(InvitationErrors.Cancelled(Id)),
            var status when status == InvitationStatus.Expired => Result.Failure<Client>(InvitationErrors.Expired(Id)),
            _ => AcceptInternal(clientId, email, firstName, lastName)
        };
    }

    /// <summary>
    /// Cancels the invitation.
    /// </summary>
    /// <returns>The result of the cancellation operation.</returns>
    public Result Cancel() =>
        Status switch
        {
            var status when status == InvitationStatus.Cancelled => Result.Failure<Client>(InvitationErrors.Cancelled(Id)),
            var status when status == InvitationStatus.Expired => Result.Failure<Client>(InvitationErrors.Expired(Id)),
            _ => CancelInternal()
        };

    /// <summary>
    /// Creates a new invitation for the specified trainer and the client with the specified email.
    /// </summary>
    /// <param name="trainer">The trainer.</param>
    /// <param name="email">The email.</param>
    /// <returns>The new invitation instance.</returns>
    internal static Result<Invitation> Create(Trainer trainer, string email)
    {
        var invitation = new Invitation(
            new InvitationId(Guid.NewGuid()),
            trainer.Id,
            email,
            new Sender(trainer.FirstName, trainer.LastName),
            InvitationStatus.Pending);

        invitation.RaiseDomainEvent(new InvitationCreatedDomainEvent(
            Guid.NewGuid(),
            SystemTimeProvider.UtcNow(),
            invitation.Id,
            invitation.TrainerId,
            invitation.Email,
            invitation.Sender));

        return invitation;
    }

    private Result<Client> AcceptInternal(ClientId clientId, string email, string firstName, string lastName)
    {
        Status = InvitationStatus.Accepted;

        return Client.Create(clientId, TrainerId, email, firstName, lastName);
    }

    private Result CancelInternal()
    {
        Status = InvitationStatus.Cancelled;

        RaiseDomainEvent(new InvitationCancelledDomainEvent(Guid.NewGuid(), SystemTimeProvider.UtcNow(), Id));

        return Result.Success();
    }
}
