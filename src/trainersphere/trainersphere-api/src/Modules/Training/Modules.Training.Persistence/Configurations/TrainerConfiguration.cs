using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.Training.Domain.Trainers;
using Modules.Training.Persistence.Constants;
using Shared.Extensions;

namespace Modules.Training.Persistence.Configurations;

/// <summary>
/// Represents the <see cref="Trainer"/> entity configuration.
/// </summary>
internal sealed class TrainerConfiguration : IEntityTypeConfiguration<Trainer>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<Trainer> builder) =>
        builder
            .Tap(ConfigureDataStructure)
            .Tap(ConfigureIndexes);

    private static void ConfigureDataStructure(EntityTypeBuilder<Trainer> builder)
    {
        builder.ToTable(TableNames.Trainers);

        builder.HasKey(trainer => trainer.Id);

        builder.Property(trainer => trainer.Id)
            .ValueGeneratedNever()
            .HasConversion(trainerId => trainerId.Value, value => new TrainerId(value));

        builder.Property(trainer => trainer.Email).IsRequired().HasMaxLength(300);

        builder.Property(trainer => trainer.FirstName).IsRequired().HasMaxLength(100);

        builder.Property(trainer => trainer.LastName).IsRequired().HasMaxLength(100);

        builder.Property(trainer => trainer.CreatedOnUtc).IsRequired();

        builder.Property(trainer => trainer.ModifiedOnUtc).IsRequired(false);
    }

    private static void ConfigureIndexes(EntityTypeBuilder<Trainer> builder) =>
        builder.HasIndex(trainer => trainer.Email).IsUnique();
}
