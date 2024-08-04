using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.Users.Domain.Roles;
using Modules.Users.Persistence.Constants;
using Shared.Extensions;

namespace Modules.Users.Persistence.Configurations;

/// <summary>
/// Represents the <see cref="RolePermission"/> entity configuration.
/// </summary>
internal sealed class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermission>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<RolePermission> builder) =>
        builder
            .Tap(ConfigureDataStructure)
            .Tap(ConfigureRelationships)
            .Tap(ConfigureIndexes)
            .Tap(ConfigureData);

    private static void ConfigureDataStructure(EntityTypeBuilder<RolePermission> builder)
    {
        builder.ToTable(TableNames.RolePermissions);

        builder.HasKey(rolePermission => new { rolePermission.RoleId, rolePermission.PermissionId });
    }

    private static void ConfigureRelationships(EntityTypeBuilder<RolePermission> builder)
    {
        builder.HasOne<Role>()
            .WithMany()
            .HasForeignKey(rolePermission => rolePermission.RoleId)
            .IsRequired();

        builder.HasOne<Permission>()
            .WithMany()
            .HasForeignKey(rolePermission => rolePermission.PermissionId)
            .IsRequired();
    }

    private static void ConfigureIndexes(EntityTypeBuilder<RolePermission> builder) =>
        builder.HasIndex(rolePermission => rolePermission.RoleId);

    private static void ConfigureData(EntityTypeBuilder<RolePermission> builder) =>
        builder.HasData(
            new RolePermission(Role.Registered, Permission.ReadUser),
            new RolePermission(Role.Registered, Permission.ModifyUser),
            new RolePermission(Role.Trainer, Permission.ReadInvitations),
            new RolePermission(Role.Trainer, Permission.CancelInvitation),
            new RolePermission(Role.Trainer, Permission.InviteClient));
}
