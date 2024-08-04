using Domain.Primitives;
using Modules.Training.Domain.Trainers;
using Shared.Results;

namespace Modules.Training.Domain.Clients;

/// <summary>
/// Represents the client entity.
/// </summary>
public sealed class Client : Entity<ClientId>, IAuditable
{
    /// <summary>
    /// The role name.
    /// </summary>
    public const string Role = "Client";

    /// <summary>
    /// Initializes a new instance of the <see cref="Client"/> class.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <param name="trainerId">The trainer identifier.</param>
    /// <param name="email">The email.</param>
    /// <param name="firstName">The first name.</param>
    /// <param name="lastName">The last name.</param>
    private Client(ClientId id, TrainerId trainerId, string email, string firstName, string lastName)
        : base(id)
    {
        TrainerId = trainerId;
        Email = email;
        FirstName = firstName;
        LastName = lastName;
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
    /// Changes the client's basic information.
    /// </summary>
    /// <param name="firstName">The first name.</param>
    /// <param name="lastName">The last name.</param>
    public void Change(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }

    /// <summary>
    /// Creates a new client with the specified parameters.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <param name="trainerId">The trainer identifier.</param>
    /// <param name="email">The email.</param>
    /// <param name="firstName">The first name.</param>
    /// <param name="lastName">The last name.</param>
    /// <returns>The new trainer instance.</returns>
    internal static Result<Client> Create(ClientId id, TrainerId trainerId, string email, string firstName, string lastName)
    {
        var client = new Client(id, trainerId, email, firstName, lastName);

        return client;
    }
}
