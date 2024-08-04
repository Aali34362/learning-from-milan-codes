using System.Net.Http.Headers;
using Domain.Followers;
using Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations;

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);

        builder.ComplexProperty(
            u => u.Email,
            b => b.Property(e => e.Value).HasColumnName(nameof(User.Email)));

        builder.ComplexProperty(
            u => u.Name,
            b => b.Property(e => e.Value).HasColumnName(nameof(User.Name)));
    }
}

internal sealed class FollowerConfiguration : IEntityTypeConfiguration<Follower>
{
    public void Configure(EntityTypeBuilder<Follower> builder)
    {
        builder.HasKey(f => new { f.UserId, f.FollowedId });

        builder.HasIndex(f => new { f.FollowedId, f.UserId });

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(f => f.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(f => f.FollowedId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Ignore(f => f.Id);
    }
}
