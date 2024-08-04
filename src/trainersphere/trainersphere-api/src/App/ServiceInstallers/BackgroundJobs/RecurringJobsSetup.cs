using Infrastructure.BackgroundJobs;
using Microsoft.Extensions.Options;
using Quartz;
using Shared.Extensions;

namespace App.ServiceInstallers.BackgroundJobs;

/// <summary>
/// Represents the <see cref="QuartzOptions"/> setup.
/// </summary>
internal sealed class RecurringJobsSetup : IConfigureOptions<QuartzOptions>
{
    private readonly IEnumerable<IRecurringJobConfiguration> _recurringJobConfigurations;

    /// <summary>
    /// Initializes a new instance of the <see cref="RecurringJobsSetup"/> class.
    /// </summary>
    /// <param name="recurringJobConfigurations">The recurring job configurations.</param>
    public RecurringJobsSetup(IEnumerable<IRecurringJobConfiguration> recurringJobConfigurations) =>
        _recurringJobConfigurations = recurringJobConfigurations;

    /// <inheritdoc />
    public void Configure(QuartzOptions options) =>
        _recurringJobConfigurations.ForEach(configuration =>
            options
                .AddJob(
                    configuration.Type,
                    jobBuilder => jobBuilder.WithIdentity(configuration.Name))
                .AddTrigger(triggerBuilder =>
                    triggerBuilder
                        .ForJob(configuration.Name)
                        .WithSimpleSchedule(scheduleBuilder =>
                            scheduleBuilder
                                .WithIntervalInSeconds(configuration.IntervalInSeconds)
                                .RepeatForever())));
}
