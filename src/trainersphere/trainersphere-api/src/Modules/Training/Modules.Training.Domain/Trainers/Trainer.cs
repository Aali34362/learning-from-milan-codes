using Domain.Primitives;
using Modules.Training.Domain.Invitations;
using Shared.Results;

namespace Modules.Training.Domain.Trainers;

/// <summary>
/// Represents the trainer entity.
/// </summary>
public sealed class Trainer : Entity<TrainerId>, IAuditable
{
    /// <summary>
    /// The role name.
    /// </summary>
    public const string Role = "Trainer";

    /// <summary>
    /// Initializes a new instance of the <see cref="Trainer"/> class.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <param name="email">The email.</param>
    /// <param name="firstName">The first name.</param>
    /// <param name="lastName">The last name.</param>
    private Trainer(TrainerId id, string email, string firstName, string lastName)
        : base(id)
    {
        Email = email;
        FirstName = firstName;
        LastName = lastName;
    }

    /// <summary>
    /// Gets the email.
    /// </summary>
    public string Email { get; private set; }

    /// <summary>
    /// Gets the first name.
    /// </summary>
    public string FirstName { get; private set; }

    /// <summary>
    /// Gets the last name.
    /// </summary>
    public string LastName { get; private set; }

    /// <inheritdoc />
    public DateTime CreatedOnUtc { get; private set; }

    /// <inheritdoc />
    public DateTime? ModifiedOnUtc { get; private set; }

    /// <summary>
    /// Creates a new trainer with the specified parameters.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <param name="email">The email.</param>
    /// <param name="firstName">The first name.</param>
    /// <param name="lastName">The last name.</param>
    /// <returns>The new trainer instance.</returns>
    public static Result<Trainer> Create(TrainerId id, string email, string firstName, string lastName)
    {
        var trainer = new Trainer(id, email, firstName, lastName);

        return trainer;
    }

    /// <summary>
    /// Changes the trainer's basic information.
    /// </summary>
    /// <param name="firstName">The first name.</param>
    /// <param name="lastName">The last name.</param>
    public void Change(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }

    /// <summary>
    /// Invites the client with the specified email to train with the trainer.
    /// </summary>
    /// <param name="email">The email.</param>
    /// <returns>The newly created invitation.</returns>
    public Result<Invitation> Invite(string email) => Invitation.Create(this, email);
}
