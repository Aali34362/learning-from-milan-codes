using Microsoft.EntityFrameworkCore;
using Modules.Training.Persistence.Constants;

namespace Modules.Training.Persistence;

/// <summary>
/// Represents the training module database context.
/// </summary>
public sealed class TrainingDbContext : DbContext
{
    /// <summary>
    /// Initializes a new instance of the <see cref="TrainingDbContext"/> class.
    /// </summary>
    /// <param name="options">The database context options.</param>
    public TrainingDbContext(DbContextOptions<TrainingDbContext> options)
        : base(options)
    {
    }

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schemas.Training);

        modelBuilder.ApplyConfigurationsFromAssembly(AssemblyReference.Assembly);
    }
}
