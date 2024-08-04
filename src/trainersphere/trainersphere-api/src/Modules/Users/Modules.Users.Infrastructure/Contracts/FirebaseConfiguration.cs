// ReSharper disable InconsistentNaming

namespace Modules.Users.Infrastructure.Contracts;

/// <summary>
/// Represents the Firebase configuration.
/// </summary>
internal sealed record FirebaseConfiguration
{
    public string Type { get; init; } = string.Empty;

    public string Project_Id { get; init; } = string.Empty;

    public string Private_Key_Id { get; init; } = string.Empty;

    public string Private_Key { get; init; } = string.Empty;

    public string Client_Email { get; init; } = string.Empty;

    public string Client_Id { get; init; } = string.Empty;

    public string Auth_Uri { get; init; } = string.Empty;

    public string Token_Uri { get; init; } = string.Empty;

    public string Auth_Provider_X509_Cert_Url { get; init; } = string.Empty;

    public string Client_X509_Cert_Url { get; init; } = string.Empty;
}
