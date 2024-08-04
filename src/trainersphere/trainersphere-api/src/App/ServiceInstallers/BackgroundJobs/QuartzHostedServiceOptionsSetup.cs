using Microsoft.Extensions.Options;
using Quartz;

namespace App.ServiceInstallers.BackgroundJobs;

/// <summary>
/// Represents the <see cref="QuartzHostedServiceOptions"/> setup.
/// </summary>
internal sealed class QuartzHostedServiceOptionsSetup : IConfigureOptions<QuartzHostedServiceOptions>
{
    /// <inheritdoc />
    public void Configure(QuartzHostedServiceOptions options) => options.WaitForJobsToComplete = true;
}
