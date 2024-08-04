using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.Training.Domain.Invitations;
using Modules.Training.Domain.Trainers;
using Modules.Training.Persistence.Constants;
using Shared.Extensions;

namespace Modules.Training.Persistence.Configurations;

/// <summary>
/// Represents the <see cref="Invitation"/> entity configuration.
/// </summary>
internal sealed class InvitationConfiguration : IEntityTypeConfiguration<Invitation>
{
    /// <inheritdoc />
    public void Configure(EntityTypeBuilder<Invitation> builder) =>
        builder
            .Tap(ConfigureDataStructure)
            .Tap(ConfigureRelationships);

    private static void ConfigureDataStructure(EntityTypeBuilder<Invitation> builder)
    {
        builder.ToTable(TableNames.Invitations);

        builder.HasKey(invitation => invitation.Id);

        builder.Property(invitation => invitation.Id)
            .ValueGeneratedNever()
            .HasConversion(invitationId => invitationId.Value, value => new InvitationId(value));

        builder.Property(invitation => invitation.Email).IsRequired().HasMaxLength(300);

        builder.OwnsOne(invitation => invitation.Sender, senderBuilder =>
        {
            senderBuilder.Property(sender => sender.FirstName).IsRequired().HasMaxLength(100);

            senderBuilder.Property(sender => sender.LastName).IsRequired().HasMaxLength(100);
        });

        builder.Navigation(invitation => invitation.Sender).IsRequired();

        builder.Property(invitation => invitation.Status)
            .IsRequired()
            .HasConversion(status => status.Id, value => InvitationStatus.FromId(value)!);

        builder.Property(invitation => invitation.CreatedOnUtc).IsRequired();

        builder.Property(invitation => invitation.ModifiedOnUtc).IsRequired(false);
    }

    private static void ConfigureRelationships(EntityTypeBuilder<Invitation> builder) =>
        builder.HasOne<Trainer>()
            .WithMany()
            .HasForeignKey(invitation => invitation.TrainerId)
            .IsRequired();
}
