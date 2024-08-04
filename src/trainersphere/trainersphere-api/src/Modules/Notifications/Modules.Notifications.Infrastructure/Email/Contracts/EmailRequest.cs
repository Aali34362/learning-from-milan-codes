using Shared.Extensions;

namespace Modules.Notifications.Infrastructure.Email.Contracts;

/// <summary>
/// Represents the Mailersend email request.
/// </summary>
internal sealed record EmailRequest
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EmailRequest"/> class.
    /// </summary>
    private EmailRequest()
    {
    }

    /// <summary>
    /// Gets the from.
    /// </summary>
    public Sender From { get; init; } = new(string.Empty);

    /// <summary>
    /// Gets the the to array.
    /// </summary>
    public Recipient[] To { get; init; } = Array.Empty<Recipient>();

    /// <summary>
    /// Gets the variables.
    /// </summary>
    public Variable[] Variables { get; init; } = Array.Empty<Variable>();

    /// <summary>
    /// Gets the template identifier.
    /// </summary>
    public string TemplateId { get; init; } = string.Empty;

    /// <summary>
    /// Creates a new email request instance with the specified template identifier.
    /// </summary>
    /// <param name="templateId">The template identifier.</param>
    /// <returns>The newly created email request instance.</returns>
    public static EmailRequest Create(string templateId) => new()
    {
        TemplateId = templateId
    };

    /// <summary>
    /// Creates a new email request instance from the existing instance with the specified sender.
    /// </summary>
    /// <param name="email">The sender email.</param>
    /// <returns>The newly created email request instance.</returns>
    public EmailRequest WithSender(string email) => this with
    {
        From = new Sender(email)
    };

    /// <summary>
    /// Creates a new email request instance from the existing instance with the specified recipient.
    /// </summary>
    /// <param name="email">The recipient email.</param>
    /// <returns>The newly created email request instance.</returns>
    public EmailRequest WithRecipient(string email) => this with
    {
        To = new List<Recipient> { new(email) }
            .Tap(list => list.AddRange(To))
            .ToArray()
    };

    /// <summary>
    /// Creates a new email request instance from the existing instance with the specified variable.
    /// </summary>
    /// <param name="variable">The variable.</param>
    /// <returns>The newly created email request instance.</returns>
    public EmailRequest WithVariable(Variable variable) => this with
    {
        Variables = new List<Variable> { variable }
            .Tap(list => list.AddRange(Variables))
            .ToArray()
    };
}
