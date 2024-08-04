using Application.Abstractions.Idempotency;
using MediatR;

namespace Application.Behaviors;

internal sealed class IdempotentCommandPipelineBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IdempotentCommand
{
    private readonly IIdempotencyService _idempotencyService;

    public IdempotentCommandPipelineBehavior(IIdempotencyService idempotencyService)
    {
        _idempotencyService = idempotencyService;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (await _idempotencyService.RequestExistsAsync(request.RequestId))
        {
            return default;
        }

        await _idempotencyService.CreateRequestAsync(request.RequestId, typeof(TRequest).Name);

        var response = await next();

        return response;
    }
}
