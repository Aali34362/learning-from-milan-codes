using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.Training.Domain.Clients;
using Modules.Training.Domain.Trainers;
using Modules.Training.Persistence.Constants;
using Shared.Extensions;

namespace Modules.Training.Persistence.Configurations;

/// <summary>
/// Represents the <see cref="Client"/> entity configuration.
/// </summary>
internal sealed class ClientConfiguration : IEntityTypeConfiguration<Client>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<Client> builder) =>
        builder
            .Tap(ConfigureDataStructure)
            .Tap(ConfigureRelationships)
            .Tap(ConfigureIndexes);

    private static void ConfigureDataStructure(EntityTypeBuilder<Client> builder)
    {
        builder.ToTable(TableNames.Clients);

        builder.HasKey(client => client.Id);

        builder.Property(client => client.Id)
            .ValueGeneratedNever()
            .HasConversion(clientId => clientId.Value, value => new ClientId(value));

        builder.Property(client => client.Email).IsRequired().HasMaxLength(300);

        builder.Property(client => client.FirstName).IsRequired().HasMaxLength(100);

        builder.Property(client => client.LastName).IsRequired().HasMaxLength(100);

        builder.Property(client => client.CreatedOnUtc).IsRequired();

        builder.Property(client => client.ModifiedOnUtc).IsRequired(false);
    }

    private static void ConfigureRelationships(EntityTypeBuilder<Client> builder) =>
        builder.HasOne<Trainer>()
            .WithMany()
            .HasForeignKey(client => client.TrainerId)
            .IsRequired();

    private static void ConfigureIndexes(EntityTypeBuilder<Client> builder) =>
        builder.HasIndex(trainer => trainer.Email).IsUnique();
}
