using App.StartupTasks;
using Infrastructure.Configuration;

namespace App.ServiceInstallers.StartupTasks;

/// <summary>
/// Represents the startup tasks service installer.
/// </summary>
internal sealed class StartupTasksServiceInstaller : IServiceInstaller
{
    /// <inheritdoc />
    public void Install(IServiceCollection services, IConfiguration configuration) => services.AddHostedService<MigrateDatabaseStartupTask>();
}
