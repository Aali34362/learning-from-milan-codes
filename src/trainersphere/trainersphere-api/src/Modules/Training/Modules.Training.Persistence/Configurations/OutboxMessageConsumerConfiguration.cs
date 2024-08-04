using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modules.Training.Persistence.Constants;
using Persistence.Outbox;

namespace Modules.Training.Persistence.Configurations;

/// <summary>
/// Represents the <see cref="OutboxMessageConsumer"/> entity configuration.
/// </summary>
internal sealed class OutboxMessageConsumerConfiguration : IEntityTypeConfiguration<OutboxMessageConsumer>
{
    public void Configure(EntityTypeBuilder<OutboxMessageConsumer> builder) => ConfigureDataStructure(builder);

    private static void ConfigureDataStructure(EntityTypeBuilder<OutboxMessageConsumer> builder)
    {
        builder.ToTable(TableNames.OutboxMessageConsumers);

        builder.HasKey(outboxMessageConsumer => new { outboxMessageConsumer.Id, outboxMessageConsumer.Name });
    }
}
