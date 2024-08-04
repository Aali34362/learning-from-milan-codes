using Serilog;
using Shared.Extensions;

namespace App.Utility;

/// <summary>
/// Contains utility methods for logging.
/// </summary>
internal static class LoggingUtility
{
    /// <summary>
    /// Wraps the provided startup action with a try-catch-finally block and provides logging.
    /// </summary>
    /// <param name="startupAction">The startup action.</param>
    internal static void Run(Action startupAction)
    {
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .CreateBootstrapLogger();

        Log.Information("Starting up.");

        FunctionalExtensions.TryCatchFinally(
            startupAction,
            exception => Log.Fatal(exception, "Unhandled exception."),
            () =>
            {
                Log.Information("Shutting down.");
                Log.CloseAndFlush();
            });
    }
}
