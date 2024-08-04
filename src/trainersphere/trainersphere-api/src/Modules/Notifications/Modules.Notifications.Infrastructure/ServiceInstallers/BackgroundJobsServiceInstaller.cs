using Infrastructure.BackgroundJobs;
using Infrastructure.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;
using Shared.Extensions;

namespace Modules.Notifications.Infrastructure.ServiceInstallers;

/// <summary>
/// Represents the notifications module background jobs service installer.
/// </summary>
internal sealed class BackgroundJobsServiceInstaller : IServiceInstaller
{
    /// <inheritdoc />
    public void Install(IServiceCollection services, IConfiguration configuration) =>
        services
            .ConfigureOptions<ProcessInboxMessagesOptionsSetup>()
            .Tap(AddRecurringJobConfigurations);

    private static void AddRecurringJobConfigurations(IServiceCollection services) =>
        services.Scan(scan =>
            scan.FromAssemblies(AssemblyReference.Assembly)
                .AddClasses(filter => filter.Where(type => type.IsAssignableTo(typeof(IRecurringJobConfiguration))), false)
                .UsingRegistrationStrategy(RegistrationStrategy.Append)
                .AsImplementedInterfaces()
                .WithTransientLifetime());
}
