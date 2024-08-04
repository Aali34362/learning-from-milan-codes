namespace Application.Abstractions.Idempotency;

public interface IIdempotencyService
{
    Task<bool> RequestExistsAsync(Guid requestId);

    Task CreateRequestAsync(Guid requestId, string name);
}