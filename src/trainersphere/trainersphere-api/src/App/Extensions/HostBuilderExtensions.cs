using Serilog;

namespace App.Extensions;

/// <summary>
/// Contains extensions methods for the <see cref="IHostBuilder"/> interface.
/// </summary>
internal static class HostBuilderExtensions
{
    /// <summary>
    /// Configures Serilog as a logging providers using the configuration defined in the application settings.
    /// </summary>
    /// <param name="hostBuilder">The host builder.</param>
    internal static void UseSerilogWithConfiguration(this IHostBuilder hostBuilder) =>
        hostBuilder.UseSerilog((hostBuilderContext, loggerConfiguration) =>
            loggerConfiguration.ReadFrom.Configuration(hostBuilderContext.Configuration));
}
