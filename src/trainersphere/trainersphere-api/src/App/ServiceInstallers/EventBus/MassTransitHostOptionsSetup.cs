using MassTransit;
using Microsoft.Extensions.Options;

namespace App.ServiceInstallers.EventBus;

/// <summary>
/// Represents the <see cref="MassTransitHostOptions"/> setup.
/// </summary>
internal sealed class MassTransitHostOptionsSetup : IConfigureOptions<MassTransitHostOptions>
{
    /// <inheritdoc />
    public void Configure(MassTransitHostOptions options) => options.WaitUntilStarted = true;
}
