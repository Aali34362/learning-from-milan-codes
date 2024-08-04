namespace Modules.Training.Persistence.Constants;

/// <summary>
/// Represents the table names in the training module.
/// </summary>
internal static class TableNames
{
    /// <summary>
    /// The trainers table.
    /// </summary>
    internal const string Trainers = "trainers";

    /// <summary>
    /// The clients table.
    /// </summary>
    internal const string Clients = "clients";

    /// <summary>
    /// The invitations table.
    /// </summary>
    internal const string Invitations = "invitations";

    /// <summary>
    /// The inbox messages table.
    /// </summary>
    internal const string InboxMessages = "inbox_messages";

    /// <summary>
    /// The inbox message consumers table.
    /// </summary>
    internal const string InboxMessageConsumers = "inbox_message_consumers";

    /// <summary>
    /// The outbox messages table.
    /// </summary>
    internal const string OutboxMessages = "outbox_messages";

    /// <summary>
    /// The outbox message consumers table.
    /// </summary>
    internal const string OutboxMessageConsumers = "outbox_message_consumers";
}
