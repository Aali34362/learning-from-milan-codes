using Gatherly.Application.Abstractions.Messaging;

namespace Gatherly.Application.Gatherings.CancelGathering;

public sealed record CancelGatheringCommand(Guid GatheringId) : ICommand;
