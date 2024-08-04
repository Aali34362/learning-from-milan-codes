using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.Users.Persistence.Constants;
using Persistence.Inbox;

namespace Modules.Users.Persistence.Configurations;

/// <summary>
/// Represents the <see cref="InboxMessageConsumer"/> entity configuration.
/// </summary>
internal sealed class InboxMessageConsumerConfiguration : IEntityTypeConfiguration<InboxMessageConsumer>
{
    public void Configure(EntityTypeBuilder<InboxMessageConsumer> builder) => ConfigureDataStructure(builder);

    private static void ConfigureDataStructure(EntityTypeBuilder<InboxMessageConsumer> builder)
    {
        builder.ToTable(TableNames.InboxMessageConsumers);

        builder.HasKey(inboxMessageConsumer => new { inboxMessageConsumer.Id, inboxMessageConsumer.Name });
    }
}
