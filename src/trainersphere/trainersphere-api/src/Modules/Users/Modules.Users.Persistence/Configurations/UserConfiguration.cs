using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.Users.Domain.Users;
using Modules.Users.Persistence.Constants;
using Shared.Extensions;

namespace Modules.Users.Persistence.Configurations;

/// <summary>
/// Represents the <see cref="User"/> entity configuration.
/// </summary>
internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<User> builder) =>
        builder
            .Tap(ConfigureDataStructure)
            .Tap(ConfigureRelationships)
            .Tap(ConfigureIndexes);

    private static void ConfigureDataStructure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable(TableNames.Users);

        builder.HasKey(user => user.Id);

        builder.Property(user => user.Id).ValueGeneratedNever().HasConversion(userId => userId.Value, value => new UserId(value));

        builder.Property(user => user.IdentityProviderId).IsRequired().HasMaxLength(500);

        builder.Property(user => user.Email).IsRequired().HasMaxLength(300);

        builder.Property(user => user.FirstName).IsRequired().HasMaxLength(100);

        builder.Property(user => user.LastName).IsRequired().HasMaxLength(100);

        builder.Property(user => user.CreatedOnUtc).IsRequired();

        builder.Property(user => user.ModifiedOnUtc).IsRequired(false);
    }

    private static void ConfigureRelationships(EntityTypeBuilder<User> builder) =>
        builder.HasMany(user => user.Roles)
            .WithMany(role => role.Users)
            .UsingEntity<UserRole>();

    private static void ConfigureIndexes(EntityTypeBuilder<User> builder)
    {
        builder.HasIndex(user => user.Email).IsUnique();

        builder.HasIndex(user => user.IdentityProviderId).IsUnique();
    }
}
