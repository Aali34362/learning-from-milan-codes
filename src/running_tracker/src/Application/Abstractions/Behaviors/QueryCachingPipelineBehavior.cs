using Application.Abstractions.Caching;
using MediatR;

namespace Application.Abstractions.Behaviors;

internal sealed class QueryCachingPipelineBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : ICachedQuery
{
    private readonly ICacheService _cacheService;

    public QueryCachingPipelineBehavior(ICacheService cacheService)
    {
        _cacheService = cacheService;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        return await _cacheService.GetOrCreateAsync(
            request.CacheKey,
            _ => next(),
            request.Expiration,
            cancellationToken);
    }
}
