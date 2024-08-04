using MediatR;

namespace Application.Abstractions.Idempotency;

public abstract record IdempotentCommand(Guid RequestId) : IRequest;
