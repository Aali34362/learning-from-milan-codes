using Microsoft.EntityFrameworkCore;
using Modules.Notifications.Persistence.Constants;

namespace Modules.Notifications.Persistence;

/// <summary>
/// Represents the notifications module database context.
/// </summary>
public sealed class NotificationsDbContext : DbContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="NotificationsDbContext"/> class.
    /// </summary>
    /// <param name="options">The database context options.</param>
    public NotificationsDbContext(DbContextOptions<NotificationsDbContext> options)
        : base(options)
    {
    }

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schemas.Notifications);

        modelBuilder.ApplyConfigurationsFromAssembly(AssemblyReference.Assembly);
    }
}
