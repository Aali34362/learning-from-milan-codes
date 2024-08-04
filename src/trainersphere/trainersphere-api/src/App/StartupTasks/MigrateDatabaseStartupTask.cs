using Microsoft.EntityFrameworkCore;
using Modules.Notifications.Persistence;
using Modules.Training.Persistence;
using Modules.Users.Persistence;

namespace App.StartupTasks;

/// <summary>
/// Represents the startup task for migrating the database in the development environment only.
/// </summary>
internal sealed class MigrateDatabaseStartupTask : BackgroundService
{
    private readonly IHostEnvironment _environment;
    private readonly IServiceProvider _serviceProvider;

    /// <summary>
    /// Initializes a new instance of the <see cref="MigrateDatabaseStartupTask"/> class.
    /// </summary>
    /// <param name="environment">The environment.</param>
    /// <param name="serviceProvider">The service provider.</param>
    public MigrateDatabaseStartupTask(IHostEnvironment environment, IServiceProvider serviceProvider)
    {
        _environment = environment;
        _serviceProvider = serviceProvider;
    }

    /// <inheritdoc />
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        if (!_environment.IsDevelopment())
        {
            return;
        }

        using IServiceScope scope = _serviceProvider.CreateScope();

        await MigrateDatabaseAsync<UsersDbContext>(scope, stoppingToken);

        await MigrateDatabaseAsync<TrainingDbContext>(scope, stoppingToken);

        await MigrateDatabaseAsync<NotificationsDbContext>(scope, stoppingToken);
    }

    private static async Task MigrateDatabaseAsync<TDbContext>(IServiceScope scope, CancellationToken cancellationToken)
        where TDbContext : DbContext
    {
        TDbContext dbContext = scope.ServiceProvider.GetRequiredService<TDbContext>();

        await dbContext.Database.MigrateAsync(cancellationToken);
    }
}
