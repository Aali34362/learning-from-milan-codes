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
